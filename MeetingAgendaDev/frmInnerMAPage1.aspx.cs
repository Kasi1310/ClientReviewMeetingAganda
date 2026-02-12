using ClientMeetingAgenda.App_Code;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using Image = iTextSharp.text.Image;

namespace ClientMeetingAgenda
{
    public partial class frmInnerMAPage1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<clsOutput> SaveMeetingAgenda(clsMeetingAgenda clsMeetingAgenda)
        {
            DataTable dtAttendeesInvited;
            clsMeetingAgenda objclsMeetingAgenda = new clsMeetingAgenda();
            List<clsOutput> lstclsOutput = new List<clsOutput>();
            //var pdf_FileName = HttpContext.Current.Session["PDFFileName"].ToString();
            var pdf_FileName = HttpContext.Current.Session["PDFFileName"]?.ToString();
            bool IsPDFGenerated = !string.IsNullOrWhiteSpace(pdf_FileName);
            objclsMeetingAgenda.IsPDFGenerated = IsPDFGenerated;
            //clsMeetingAgenda.IsPDFGenerated

            //if (!string.IsNullOrEmpty(pdf_FileName))
            //{
            //    clsMeetingAgenda.IsPDFGenerated = true;
            //}
            //else
            //{
            //    clsMeetingAgenda.IsPDFGenerated = false;
            //}
            if (clsMeetingAgenda.IsPrint)
            {
                //string PDFPath = NewGeneratePDF("","", ""); //GeneratePDF(clsMeetingAgenda);

                clsOutput objclsOutput = new clsOutput();
                objclsOutput.MeetingAgendaID = 0;
                objclsOutput.SignatureID = 0;

               //HttpContext.Current.Session["PrintDocument"] = PDFPath;

                lstclsOutput.Add(objclsOutput);
            }
            else
            {

                objclsMeetingAgenda = clsMeetingAgenda;
                objclsMeetingAgenda.LastUpdatedBy = HttpContext.Current.Session["UserName"]?.ToString().Trim();
                objclsMeetingAgenda.FileName = pdf_FileName;
                objclsMeetingAgenda.IsPDFGenerated = IsPDFGenerated;

                DataSet dsMeetingAgenda = new DataSet();
                dsMeetingAgenda = objclsMeetingAgenda.InsertUpdateMeetingAgenda();

                HttpContext.Current.Session["dsMeetingAgenda"] = dsMeetingAgenda;

                HttpContext.Current.Session["ssnMAID"] = null;

                if (dsMeetingAgenda != null && dsMeetingAgenda.Tables.Count == 3 && dsMeetingAgenda.Tables[0] != null && HttpContext.Current.Session["dtAttendeesInvited"] != null)
                {
                    HttpContext.Current.Session["ssnMAID"] = dsMeetingAgenda.Tables[0].Rows[0]["ID"].ToString();

                    dtAttendeesInvited = new DataTable();
                    dtAttendeesInvited = (DataTable)HttpContext.Current.Session["dtAttendeesInvited"];

                    DataTable dtAttendeesFromDB = new DataTable();
                    objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                    dtAttendeesFromDB = objclsMeetingAgenda.SelectAttendes();

                    objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                    objclsMeetingAgenda.DeleteAttendes();

                    bool IsSurveyMailSend;
                    int intAttendeesID;

                    for (int i = 0; i < dtAttendeesInvited.Rows.Count; i++)
                    {
                        IsSurveyMailSend = false;
                        intAttendeesID = 0;
                        for (int j = 0; j < dtAttendeesFromDB.Rows.Count; j++)
                        {
                            if (dtAttendeesInvited.Rows[i]["Email"].ToString().Trim() == dtAttendeesFromDB.Rows[j]["Email"].ToString().Trim())
                            {
                                IsSurveyMailSend = Convert.ToBoolean(dtAttendeesFromDB.Rows[j]["IsSurveyMailSend"].ToString().Trim());
                            }
                        }

                        dtAttendeesInvited.Rows[i]["IsSurveyMailSend"] = IsSurveyMailSend;
                        dtAttendeesInvited.Rows[i]["MeetingAgendaID"] = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());

                        objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                        objclsMeetingAgenda.AttendeesName = dtAttendeesInvited.Rows[i]["Name"].ToString().Trim();
                        objclsMeetingAgenda.AttendeesTitle = dtAttendeesInvited.Rows[i]["Title"].ToString().Trim();
                        objclsMeetingAgenda.AttendeesEmail = dtAttendeesInvited.Rows[i]["Email"].ToString().Trim();
                        objclsMeetingAgenda.AttendeesPhone = dtAttendeesInvited.Rows[i]["Phone"].ToString().Trim();
                        objclsMeetingAgenda.IsSurveyMailSend = IsSurveyMailSend;
                        objclsMeetingAgenda.AttendedMeeting = dtAttendeesInvited.Rows[i]["AttendedMeeting"].ToString().Trim();
                        intAttendeesID = objclsMeetingAgenda.InsertAttendes();

                        dtAttendeesInvited.Rows[i]["ID"] = intAttendeesID;
                    }

                    HttpContext.Current.Session["dtAttendeesInvited"] = dtAttendeesInvited;

                    if (objclsMeetingAgenda.lstclsSignature != null)
                    {

                        int output = 0;
                        for (int i = 0; i < objclsMeetingAgenda.lstclsSignature.Count; i++)
                        {
                            objclsMeetingAgenda.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                            objclsMeetingAgenda.SignatureID = int.Parse(objclsMeetingAgenda.lstclsSignature[i].SignatureID.ToString().Trim());
                            objclsMeetingAgenda.Patient = objclsMeetingAgenda.lstclsSignature[i].Patient.ToString().Trim();
                            objclsMeetingAgenda.Signature = objclsMeetingAgenda.lstclsSignature[i].Signature.ToString().Trim();
                            objclsMeetingAgenda.Facility = objclsMeetingAgenda.lstclsSignature[i].Facility.ToString().Trim();
                            output = objclsMeetingAgenda.InsertSignature();

                            clsOutput objclsOutput = new clsOutput();
                            objclsOutput.MeetingAgendaID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0][0].ToString().Trim());
                            objclsOutput.SignatureID = output;

                            lstclsOutput.Add(objclsOutput);
                        }
                    }
                }

                //if (objclsMeetingAgenda.IsPDFGenerated)
                //{
                //    HttpContext.Current.Session["FileDownload"] = GeneratePDF(objclsMeetingAgenda);

                //    objclsMeetingAgenda = new clsMeetingAgenda();
                //    objclsMeetingAgenda.ID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0]["ID"].ToString());
                //    objclsMeetingAgenda.FileName = HttpContext.Current.Session["FileDownload"].ToString();
                //    objclsMeetingAgenda.LastUpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                //    objclsMeetingAgenda.UpdatePDFStatus("");
                //}
            }


            return lstclsOutput;
        }

        [WebMethod]
        public static string NewGeneratePDF(string formHtml, string clientName, string clientNumber)
        {
            string htmlPath = null;
            string pdfPath = null;
            string hdnFileName = null;

            try
            {
                htmlPath = SaveHtmlToTempFile(formHtml, clientName, clientNumber);
                byte[] pdfBytes = ConvertHtmlToPdf(htmlPath);
                pdfPath = System.IO.Path.ChangeExtension(htmlPath, ".pdf");
                hdnFileName = pdfPath;
               HttpContext.Current.Session["PDFFileName"] = pdfPath;

                return Convert.ToBase64String(pdfBytes);
            }
            finally
            {
                if (!string.IsNullOrEmpty(htmlPath) && System.IO.File.Exists(htmlPath))
                    System.IO.File.Delete(htmlPath);

                //if (!string.IsNullOrEmpty(pdfPath) && System.IO.File.Exists(pdfPath))
                //    System.IO.File.Delete(pdfPath);
            }
        }

        private static string SaveHtmlToTempFile(string html, string clientName, string clientNumber)
        {

            //HttpContext.Current.Server.MapPath("~/Temp/");
            string folder = ConfigurationManager.AppSettings["upload.file.path"].ToString();// "E:\\CMS_DATA\\Contracts\\MeetingAgenda\\Test\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string currentDate = DateTime.Now.ToString("MM-dd-yyyy");

            string fileName = $"{clientNumber}_{clientName}_MeetingAgenda_{currentDate}_{Guid.NewGuid()}.html";
            string path = System.IO.Path.Combine(folder, fileName);
            System.IO.File.WriteAllText(path, html, Encoding.UTF8);
            return path;
        }

        private static byte[] ConvertHtmlToPdf(string htmlFilePath)
        {
            string wkhtmlPath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe"; // Adjust path if needed

            string inputFile = "file:///" + htmlFilePath.Replace("\\", "/");
            string outputPdf = System.IO.Path.ChangeExtension(htmlFilePath, ".pdf");

            var startInfo = new ProcessStartInfo
            {
                FileName = wkhtmlPath,
                Arguments = $"--enable-local-file-access --viewport-size 1280x1024 --zoom 1.0 --page-size A4 --print-media-type \"{inputFile}\" \"{outputPdf}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var proc = Process.Start(startInfo))
            {
                string err = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (proc.ExitCode != 0)
                    throw new Exception("wkhtmltopdf failed: " + err);
            }

            return System.IO.File.ReadAllBytes(outputPdf);
        }
        ////////////////////////////////////////////////////////

        [WebMethod]
        public static string UpdateMeetingCompleteStatus(int MAID, string UserName, string From)
        {
            if (From == "Windows")
            {
                //GeneratePDF(MAID, UserName);
                return "";
            }

            DataSet ds = new DataSet();
            DataTable dtAttendeesInvited = new DataTable();
            clsMeetingAgenda objclsMeetingAgenda = new clsMeetingAgenda();

            objclsMeetingAgenda.ID = MAID;
            ds = objclsMeetingAgenda.SelectMeetingAgenda();

            if (ds == null || ds.Tables.Count != 3 || ds.Tables[0].Rows.Count == 0)
            {
                return "";
            }

            dtAttendeesInvited = ds.Tables[1];

            string Message = "";

            Message = SendSurveyMail(MAID, UserName, dtAttendeesInvited, ds.Tables[0].Rows[0]["MeetingDate"].ToString().Trim()
                , ds.Tables[0].Rows[0]["FileName"].ToString().Trim()
                , Convert.ToInt64(ds.Tables[0].Rows[0]["ZohoId"].ToString().Trim()));

            if (Message == "")
            {
                clsSendMail objclsSendMail = new clsSendMail();
                string ToMailID = ConfigurationManager.AppSettings["MeetingAgenda.pdf.mail"].ToString();
                string Attachement = "";
                Attachement = ConfigurationManager.AppSettings["upload.file.path"].ToString() + ds.Tables[0].Rows[0]["FileName"].ToString().Trim();

                objclsSendMail.SendMail(ToMailID, "", ""
                    , ds.Tables[0].Rows[0]["ClientNo"].ToString().Trim() + "_" + ds.Tables[0].Rows[0]["ClientName"].ToString().Trim() + "_" + "Client Review Meeting Agenda"
                    , MeetingAgendaMailBody(ds.Tables[0].Rows[0]["ClientName"].ToString().Trim()
                    , ds.Tables[0].Rows[0]["ClientNo"].ToString().Trim()
                    , ds.Tables[0].Rows[0]["MeetingDate"].ToString().Trim()), Attachement);


                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            //else
            //{
            //	ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenMessagePopup();", true);
            //}

            return Message;
        }

        private static string SendSurveyMail(int MAID, string UserName, DataTable dtAttendeesInvited, string MeetingDate, string FileName, long ZohoId)
        {
            string ToErrorMailID = ConfigurationManager.AppSettings["MeetingAgenda.Error.mail"].ToString();
            string Message = "";
            bool isMailSend = false;
            int MailSendCount = 0;

            clsSendMail objclsSendMail = new clsSendMail();
            clsMeetingAgenda objclsMeetingAgenda = new clsMeetingAgenda();

            for (int i = 0; i < dtAttendeesInvited.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(dtAttendeesInvited.Rows[i]["IsSurveyMailSend"].ToString().Trim()))
                {
                    isMailSend = objclsSendMail.SendMail(dtAttendeesInvited.Rows[i]["Email"].ToString().Trim(), "", "", "Client Review Meeting - Quick survey", MailBody(dtAttendeesInvited.Rows[i]["ID"].ToString().Trim(), MeetingDate), "");
                    if (isMailSend)
                    {
                        MailSendCount += 1;
                        objclsMeetingAgenda.UpdateAttendesSurveyMailSendStatus(dtAttendeesInvited.Rows[i]["ID"].ToString().Trim());
                    }
                    else
                    {
                        Message = Message + " Not able to send mail for " + dtAttendeesInvited.Rows[i]["Email"].ToString().Trim() + " attendee.";
                        //ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenMessagePopup();", true);
                        //break;
                    }
                }
                else
                {
                    MailSendCount += 1;
                }
            }

            if (MailSendCount == dtAttendeesInvited.Rows.Count)
            {
                bool IsZohoUpload = false;
                try
                {
                    if (ZohoId == 0)
                    {
                        objclsSendMail.SendMail(ToErrorMailID, "", "", "Client Review Meeting - Zoho upload error", "Zoho Id is empty<br />File Name:" + FileName + "<br />", "");
                    }
                    else
                    {
                        string UploadStatus = UploadDocumentToZOHO(FileName, ZohoId);

                        //string UploadStatus = "200";

                        if (UploadStatus == "200")
                        {
                            IsZohoUpload = true;
                        }
                        //if (UploadStatus != "200")
                        else
                        {
                            objclsSendMail.SendMail(ToErrorMailID, "", "", "Client Review Meeting - Zoho upload error", "Upload Status Code:" + UploadStatus + "<br />File Name:" + FileName + "<br />", "");
                            IsZohoUpload = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objclsSendMail.SendMail(ToErrorMailID, "", "", "Client Review Meeting - Zoho upload error", "Error:" + ex.Message + "<br />File Name:" + FileName + "<br />", "");
                }

                objclsMeetingAgenda = new clsMeetingAgenda();
                objclsMeetingAgenda.ID = MAID;
                objclsMeetingAgenda.LastUpdatedBy = UserName;
                objclsMeetingAgenda.UpdateMeetingAgendaCompleteStatus(IsZohoUpload);
            }
            else
            {
                Message = Message + " Unable to upload the file to Zoho due to survey mail not send to all attendees.";

            }

            return Message;
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

        private static string MeetingAgendaMailBody(string ClientName, string ClientNo, string MeetingDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='utf-8' />");
            sb.AppendLine("<title></title>");
            sb.AppendLine("<style>.paraDesign {margin: 0in;font-size: 11.0pt;font-family: Calibri;}</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<table border='0' cellpadding='0' width='741' style='width: 556.1pt; transform: scale(0.977887, 0.977887); transform-origin: left top;' min-scale='0.9778869778869779'>");
            sb.AppendLine("<tbody>");
            //sb.AppendLine("<tr style='height:8.15pt'>");
            //sb.AppendLine("<td style='padding:.75pt .75pt .75pt .75pt; height:8.15pt'>");
            //sb.AppendLine("<p class='paraDesign'><span style='font-size:14.0pt'>Hi, </span></p>");
            //sb.AppendLine("</td>");
            //sb.AppendLine("</tr>");
            sb.AppendLine("<tr style='height:8.15pt'>");
            sb.AppendLine("<td style='border:solid #009094 3.0pt;padding:0.1in 5.4pt 0in 5.4pt;'>");
            sb.AppendLine("<p class='paraDesign'>");
            sb.AppendLine("<span style='font-size:14.0pt'>You are receiving this report to review and ensure that any items relevant to you or your department are addressed. This review includes changes in officials (authorized personnel, Chief, fiscal officers), trends (RPT, runs), address and rate changes, or any other matters that require follow-up and discussion with the account executive or senior management.</span>");
            sb.AppendLine("</p>");
            sb.AppendLine("<br />");
            sb.AppendLine("<p class='paraDesign'>");
            sb.AppendLine("<span style='font-size:14.0pt'>Please contact the account executive if you have any questions about this report. Our collective responsibility to our clients and Medicount is to stay informed and ensure nothing falls through the cracks</span>");
            sb.AppendLine("</p>");
            sb.AppendLine("<br />");
            sb.AppendLine("<p class='paraDesign'><span style='font-size:14.0pt'>&nbsp;</span></p><p class='paraDesign'><span style='font-size:14.0pt'>Thank you</span></p><p class='paraDesign'><b><span style='font-size: 14pt; color: rgb(0, 144, 148) !important;'>Medicount Management, Inc.</span></b><b></b></p>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }


        private static string UploadDocumentToZOHO(string FileName, long ZohoId)
        {
            clsInitialize objclsInitialize = new clsInitialize();
            clsAttachment objclsAttachment = new clsAttachment();

            objclsInitialize.SDKInitialize();
            return objclsAttachment.UploadAttachments("Accounts", ZohoId, ConfigurationManager.AppSettings["upload.file.path"].ToString() + FileName);

        }

        public class PDFFooter : PdfPageEventHelper
        {
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
                //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                //tabFot.SpacingAfter = 10F;
                //PdfPCell cell;
                //tabFot.TotalWidth = 300F;
                //cell = new PdfPCell(new Phrase("Header"));
                //tabFot.AddCell(cell);
                //tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //base.OnEndPage(writer, document);
                //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                //PdfPCell cell;
                //tabFot.TotalWidth = 300F;
                //cell = new PdfPCell(new Phrase("Footer"));
                //tabFot.AddCell(cell);
                //tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);

                base.OnEndPage(writer, document);

                PdfContentByte content;
                Rectangle rectangle;

                //Add border to page
                content = writer.DirectContent;
                rectangle = new Rectangle(document.PageSize);
                rectangle.Left += document.LeftMargin;
                rectangle.Right -= document.RightMargin;
                rectangle.Top -= document.TopMargin - 10f;
                rectangle.Bottom += document.BottomMargin;
                content.SetLineWidth(2);
                content.SetColorStroke(new BaseColor(0, 150, 143));
                content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                content.Stroke();





            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

    }
}