using ClientMeetingAgenda.App_Code;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

            if (clsMeetingAgenda.IsPrint)
            {
                string PDFPath = GeneratePDF(clsMeetingAgenda);

                clsOutput objclsOutput = new clsOutput();
                objclsOutput.MeetingAgendaID = 0;
                objclsOutput.SignatureID = 0;

                HttpContext.Current.Session["PrintDocument"] = PDFPath;

                lstclsOutput.Add(objclsOutput);
            }
            else
            {

                objclsMeetingAgenda = clsMeetingAgenda;
                objclsMeetingAgenda.LastUpdatedBy = HttpContext.Current.Session["UserName"].ToString().Trim();
                objclsMeetingAgenda.FileName = "";

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

                if (objclsMeetingAgenda.IsPDFGenerated)
                {
                    HttpContext.Current.Session["FileDownload"] = GeneratePDF(objclsMeetingAgenda);

                    objclsMeetingAgenda = new clsMeetingAgenda();
                    objclsMeetingAgenda.ID = int.Parse(dsMeetingAgenda.Tables[0].Rows[0]["ID"].ToString());
                    objclsMeetingAgenda.FileName = HttpContext.Current.Session["FileDownload"].ToString();
                    objclsMeetingAgenda.LastUpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                    objclsMeetingAgenda.UpdatePDFStatus("");
                }
            }


            return lstclsOutput;
        }

        //[WebMethod]
        public static string GeneratePDF(clsMeetingAgenda objclsMeetingAgenda)
        {
            //clsMeetingAgenda objclsMeetingAgenda;
            //DataSet dsMeetingAgenda;
            //DataTable dtMeetingAgenda;
            //DataTable dtAttendeesInvited;
            //DataTable dtSignature;

            //objclsMeetingAgenda = new clsMeetingAgenda();
            //dsMeetingAgenda = new DataSet();

            //dtMeetingAgenda = new DataTable();
            //dtAttendeesInvited = new DataTable();
            //dtSignature = new DataTable();

            //objclsMeetingAgenda.ID = MAID;
            //dsMeetingAgenda = objclsMeetingAgenda.SelectMeetingAgenda();
            //dtMeetingAgenda = dsMeetingAgenda.Tables[0];
            //dtAttendeesInvited = dsMeetingAgenda.Tables[1];
            //dtSignature = dsMeetingAgenda.Tables[2];

            string FileName = objclsMeetingAgenda.ClientNo + "_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".pdf";
            string designationFilePath = ConfigurationManager.AppSettings["upload.file.path"].ToString() + FileName;

            if (File.Exists(designationFilePath))
            {
                File.Delete(designationFilePath);
            }

            FileStream fs = new FileStream(designationFilePath, FileMode.Create);
            // Create an instance of the document class which represents the PDF document itself.  
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            HTMLWorker htmlparser = new HTMLWorker(document);
            // Create an instance to the PDF file by creating an instance of the PDF   
            // Writer class using the document and the filestrem in the constructor.  

            if (document == null)
            {
                document = new Document(PageSize.A4, 25, 25, 30, 30);
            }


            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            writer.PageEvent = new PDFFooter();


            // Open the document to enable you to write to the document  
            document.Open();



            BaseColor customBGGreenColor = new BaseColor(0, 150, 143);
            BaseColor customBGAshColor = new BaseColor(93, 103, 112);

            BaseFont baseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath(@"\CalibriFonts\Calibri.ttf"), "Identity-H", BaseFont.EMBEDDED);
            var fontHeader = new Font(baseFont, 12, Font.BOLD, BaseColor.WHITE);
            var fontContent = new Font(baseFont, 12, Font.NORMAL);
            var fontContentGreen = new Font(baseFont, 12, Font.BOLD, customBGGreenColor);


            ////Font fontHeader = FontFactory.GetFont("Calibri", 10, Font.BOLD, BaseColor.WHITE);
            //Font fontHeaderUnderLine = FontFactory.GetFont("Calibri", 16, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
            //Font fontSubHeaderUnderLine = FontFactory.GetFont("Calibri", 14, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
            //Font fontSubHeader = FontFactory.GetFont("Calibri", 14, Font.BOLD, BaseColor.BLACK);

            //Font fontSubHeaderWhite = FontFactory.GetFont("Calibri", 14, Font.BOLD, BaseColor.WHITE);
            ////Font fontContent = FontFactory.GetFont("Calibri Bold", 10, Font.NORMAL);
            //Font fontContentUnderLine = FontFactory.GetFont("Calibri", 11, Font.NORMAL | Font.UNDERLINE);
            //Font fontContentBold = FontFactory.GetFont("Calibri", 11, Font.BOLD);
            //Font fontContentBoldUnderLine = FontFactory.GetFont("Calibri", 11, Font.BOLD | Font.UNDERLINE);
            //Font fontSmallContent = FontFactory.GetFont("Calibri", 10, Font.NORMAL);
            //Font fontSmallContentBold = FontFactory.GetFont("Calibri", 10, Font.BOLD);



            float[] widths = new float[] { 28, 44, 28 };

            PdfPTable table = new PdfPTable(3);



            PdfPCell cell;

            Image image;

            // BaseColor customContentBaseColor = new BaseColor(230, 252, 251);

            table = new PdfPTable(3);
            table.WidthPercentage = 93f;
            table.HorizontalAlignment = 1;
            table.SetWidths(widths);

            table.SplitLate = false;
            //table.SplitRows = false;

            document.NewPage();
            document.Add(new Paragraph(20, "\u00a0"));

            image = Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/Logo.jpg"));
            image.ScaleAbsolute(300f, 80f);
            cell = new PdfPCell(image, false);
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Center, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("CLIENT REVIEW MEETING AGENDA", new Font(baseFont, 20, Font.BOLD, customBGGreenColor)));
            cell.PaddingBottom = 15f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Center, 2=Right
            table.AddCell(cell);


            PdfPTable childTable1 = new PdfPTable(3);
            float[] widthsC1 = new float[] { 10, 60, 30 };
            childTable1.SetWidths(widthsC1);

            childTable1.SplitLate = false;
            //childTable1.SplitRows = false;

            cell = new PdfPCell(new Phrase("CLIENT#", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase("CLIENT NAME", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING DATE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ClientNo, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ClientName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MeetingDate, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(childTable1);
            //cell.PaddingTop = 30f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable2 = new PdfPTable(4);
            float[] widthsC2 = new float[] { 20, 40, 20, 20 };
            childTable2.SetWidths(widthsC2);

            childTable2.SplitLate = false;
            //childTable2.SplitRows = false;

            cell = new PdfPCell(new Phrase("ACCOUNT EXECUTIVE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("EMAIL", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("PHONE #", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING TYPE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.AccExecName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.AccExecEmailID, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.AccExecPhone, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MeetingType, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(childTable2);
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable3 = new PdfPTable(3);
            float[] widthsC3 = new float[] { 25, 40, 35 };
            childTable3.SetWidths(widthsC3);

            childTable3.SplitLate = false;
            //childTable3.SplitRows = false;

            cell = new PdfPCell(new Phrase("CALL IN NUMBER", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING ID/CODE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING WEB LINK", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CallInNumber, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.MinimumHeight = 20f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MeetingID, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.MinimumHeight = 20f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MeetingWebLink, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.MinimumHeight = 20f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(childTable3);
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable4 = new PdfPTable(4);
            float[] widthsC4 = new float[] { 20, 20, 30, 30 };
            childTable4.SetWidths(widthsC4);

            childTable4.SplitLate = false;
            //childTable4.SplitRows = false;

            cell = new PdfPCell(new Phrase("ATTENDEES INVITED", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 4;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("NAME", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("TITLE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("EMAIL", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("ATTENDED MEETING", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            //if (HttpContext.Current.Session["dtAttendeesInvited"] != null)
            //{

            DataTable dtAttendeesInvited = new DataTable();
            dtAttendeesInvited = (DataTable)HttpContext.Current.Session["dtAttendeesInvited"];

            for (int i = 0; i < dtAttendeesInvited.Rows.Count; i++)
            {
                cell = new PdfPCell(new Phrase(dtAttendeesInvited.Rows[i]["Name"].ToString().Trim(), fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTable4.AddCell(cell);

                cell = new PdfPCell(new Phrase(dtAttendeesInvited.Rows[i]["Title"].ToString().Trim(), fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTable4.AddCell(cell);

                cell = new PdfPCell(new Phrase(dtAttendeesInvited.Rows[i]["Email"].ToString().Trim(), fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                //cell.BorderColor=
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTable4.AddCell(cell);

                cell = new PdfPCell(new Phrase(dtAttendeesInvited.Rows[i]["AttendedMeeting"].ToString().Trim(), fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                //cell.BorderColor=
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTable4.AddCell(cell);

            }
            //}


            //cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingBottom = 5f;
            //cell.Colspan = 1;
            //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //childTable4.AddCell(cell);

            //cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingBottom = 5f;
            //cell.Colspan = 1;
            //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //childTable4.AddCell(cell);

            //cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingBottom = 5f;
            //cell.Colspan = 1;
            //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            ////cell.BorderColor=
            //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //childTable4.AddCell(cell);


            cell = new PdfPCell(childTable4);
            // cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("CLIENT REVENUE NUMBERS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            PdfPTable childTable5 = new PdfPTable(6);
            float[] widthsC5 = new float[] { 20, 15, 20, 10, 20, 15 };
            childTable5.SetWidths(widthsC5);

            childTable5.SplitLate = false;
            //childTable5.SplitRows = false;

            cell = new PdfPCell(new Phrase("YTD REVENUE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.YTDRevenue, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("YTD TRANSPORTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.YTDTransports, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("REVENUE PER TRANSPORT", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RevenuePerTransport, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(childTable5);
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable6 = new PdfPTable(4);
            float[] widthsC6 = new float[] { 35, 35, 15, 15 };
            childTable6.SetWidths(widthsC6);

            childTable6.SplitLate = false;
            //childTable6.SplitRows = false;

            cell = new PdfPCell(new Phrase("REVIEW", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("COMMENTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("START DATE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("END DATE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("i.Charges, Payments,Adjustments and Write-offs", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CPAWComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CPAWStartDate1, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CPAWEndDate1, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CPAWStartDate2, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CPAWEndDate2, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("ii.RPT and Collection Rates", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RPTCollectionComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RPTCollectionStartDate1, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RPTCollectionEndDate1, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RPTCollectionStartDate2, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RPTCollectionEndDate2, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(childTable6);
            //cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTablePNcmd = new PdfPTable(1);
            float[] widthsCPNcmd = new float[] { 100 };
            childTablePNcmd.SetWidths(widthsCPNcmd);

            childTablePNcmd.SplitLate = false;
            //childTablePNcmd.SplitRows = false;

            cell = new PdfPCell(new Phrase("POSITIVE / NEGATIVE COMMENTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            //cell.MinimumHeight = 20f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTablePNcmd.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PNComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.MinimumHeight = 50f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTablePNcmd.AddCell(cell);

            cell = new PdfPCell(childTablePNcmd);
            //cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTableCTD = new PdfPTable(3);
            float[] widthsCPNCTD = new float[] { 28, 44, 28 };
            childTableCTD.SetWidths(widthsCPNCTD);

            childTableCTD.SplitLate = false;

            cell = new PdfPCell(new Phrase("CONTENT TO DISCUSS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("COMMENTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("ACTION TAKEN", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("1. Aging Review", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ARComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ARActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("2. Billing Rate Review", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BRRComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BRRActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("a.   Current Billing Rates", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTable7 = new PdfPTable(5);
            float[] widthsC7 = new float[] { 10, 12, 11, 6, 5 };
            childTable7.SetWidths(widthsC7);

            childTable7.SplitLate = false;
            //childTable7.SplitRows = false;

            cell = new PdfPCell(new Phrase("BLS:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BLS, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("BLS NE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BLSNE, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ALS, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS NE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ALSNE, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS2:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ALS2, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("Non-Transport:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            if (objclsMeetingAgenda.IsNonTransport.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase(" ", fontContent));
            }
            //cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mileage:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.Mileage, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            if (objclsMeetingAgenda.IsNonTransport.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase(" ", fontContent));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("Billing Rate Reviewed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 5;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            PdfPTable childTableBRR = new PdfPTable(2);
            float[] widthsBRR = new float[] { 14, 14 };
            childTableBRR.SetWidths(widthsBRR);

            childTableBRR.SplitLate = false;
            //childTable8.SplitRows = false;

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableBRR.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableBRR.AddCell(cell);

            if (objclsMeetingAgenda.BillingRateReviewed.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));

            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTableBRR.AddCell(cell);


            if (objclsMeetingAgenda.BillingRateReviewed.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));

            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTableBRR.AddCell(cell);

            cell = new PdfPCell(childTableBRR);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 5;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("BLS:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BLSReviewed, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("BLS NE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BLSNEReviewed, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ALSReviewed, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS NE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ALSNEReviewed, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS2:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ALS2Reviewed, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("Non-Transport:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            if (objclsMeetingAgenda.IsNonTransportReviewed.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase(" ", fontContent));
            }
            //cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mileage:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MileageReviewed, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            if (objclsMeetingAgenda.IsNonTransportReviewed.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase(" ", fontContent));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(childTable7);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CBRActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("b.   CUR (Customary and usual rates for the area)", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("CUR Reviewed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("Last Rate Change", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTableCURR = new PdfPTable(2);
            float[] widthsCURR = new float[] { 14, 14 };
            childTableCURR.SetWidths(widthsCURR);

            childTableCURR.SplitLate = false;
            //childTable8.SplitRows = false;

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCURR.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCURR.AddCell(cell);

            if (objclsMeetingAgenda.CURReviewed.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));

            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTableCURR.AddCell(cell);


            if (objclsMeetingAgenda.CURReviewed.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));

            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTableCURR.AddCell(cell);

            cell = new PdfPCell(childTableCURR);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.LastRateChange, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CURComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CURActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("3. Contract Status", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CSComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("Contract Current", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            PdfPTable childTable8 = new PdfPTable(2);
            float[] widthsC8 = new float[] { 14, 14 };
            childTable8.SetWidths(widthsC8);

            childTable8.SplitLate = false;
            //childTable8.SplitRows = false;

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable8.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable8.AddCell(cell);

            if (objclsMeetingAgenda.IsContractCurrent.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));

            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable8.AddCell(cell);


            if (objclsMeetingAgenda.IsContractCurrent.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));

            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable8.AddCell(cell);

            cell = new PdfPCell(childTable8);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("a.   Enforce", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            PdfPTable childTable9 = new PdfPTable(2);
            float[] widthsC9 = new float[] { 22, 22 };
            childTable9.SetWidths(widthsC9);

            childTable9.SplitLate = false;
            //childTable9.SplitRows = false;

            cell = new PdfPCell(new Phrase("Renewal Date:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RenewalDate, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(new Phrase("Current Rate:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CurrentRate, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(childTable9);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.EnforceActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            cell = new PdfPCell(new Phrase("4. Personnel Changes:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            PdfPTable childTable10 = new PdfPTable(2);
            float[] widthsC10 = new float[] { 15, 29 };
            childTable10.SetWidths(widthsC10);

            childTable10.SplitLate = false;
            //childTable10.SplitRows = false;

            cell = new PdfPCell(new Phrase("Chief:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PCChief, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fiscal Officer:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PCFiscalOfficer, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase("Authorized Official: Print Current Name(ask at Meeting)\nIf changed; Print name of new AO & date of Change", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PCAuthorizedOfficial, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(childTable10);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PCActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            Paragraph p1 = new Paragraph();
            Phrase ph1 = new Phrase("5. Demographic Changes" + "\n" + "\n", fontContentGreen);
            Phrase ph2 = new Phrase("\t" + "i.   Major Business Closed" + "\n" + "\n", fontContent);
            Phrase ph3 = new Phrase("\t" + "ii.  Nursing Home   Transports" + "\n" + "\n", fontContent);

            p1.Add(ph1);
            p1.Add(ph2);
            p1.Add(ph3);


            cell = new PdfPCell(p1);
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.DCComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.DCActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("6. New Business", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.NBComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.NBActionTaken, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("a.   Client Portal", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CPComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("Usage", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            PdfPTable childTable11 = new PdfPTable(2);
            float[] widthsC11 = new float[] { 14, 14 };
            childTable11.SetWidths(widthsC11);

            childTable11.SplitLate = false;
            //childTable11.SplitRows = false;

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable11.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable11.AddCell(cell);

            if (objclsMeetingAgenda.IsCPUsage.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable11.AddCell(cell);


            if (objclsMeetingAgenda.IsCPUsage.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable11.AddCell(cell);

            cell = new PdfPCell(childTable11);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            cell = new PdfPCell(new Phrase("b.   Receiving alerts on the home page or client portal", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.RAComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("Alerts Received", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTable12 = new PdfPTable(2);
            float[] widthsC12 = new float[] { 14, 14 };
            childTable12.SetWidths(widthsC12);

            childTable12.SplitLate = false;
            //childTable12.SplitRows = false;

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable12.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable12.AddCell(cell);

            if (objclsMeetingAgenda.IsRAAlertsReceived.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable12.AddCell(cell);

            if (objclsMeetingAgenda.IsRAAlertsReceived.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable12.AddCell(cell);

            cell = new PdfPCell(childTable12);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            cell = new PdfPCell(new Phrase("c.   Medicare Ground Ambulance Data Collection System", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MGComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("Discussed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTable13 = new PdfPTable(2);
            float[] widthsC13 = new float[] { 14, 14 };
            childTable13.SetWidths(widthsC13);

            childTable13.SplitLate = false;
            //childTable13.SplitRows = false;

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable13.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable13.AddCell(cell);

            if (objclsMeetingAgenda.IsMGDiscussed.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable13.AddCell(cell);

            if (objclsMeetingAgenda.IsMGDiscussed.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable13.AddCell(cell);

            cell = new PdfPCell(childTable13);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("d.   Client Patient Survey Program", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.CPSComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("Discussed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTable14 = new PdfPTable(2);
            float[] widthsC14 = new float[] { 14, 14 };
            childTable14.SetWidths(widthsC14);

            childTable14.SplitLate = false;
            //childTable14.SplitRows = false;

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable14.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable14.AddCell(cell);

            if (objclsMeetingAgenda.IsCPSDiscussed.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable14.AddCell(cell);

            if (objclsMeetingAgenda.IsCPSDiscussed.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable14.AddCell(cell);

            cell = new PdfPCell(childTable14);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            PdfPTable childTable15 = new PdfPTable(8);
            float[] widthsC15 = new float[] { 28, 24, 5, 5, 5, 5, 14, 14 };
            childTable15.SetWidths(widthsC15);

            childTable15.SplitLate = false;
            //childTable15.SplitRows = false;

            cell = new PdfPCell(new Phrase("e.   Signature Capture", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 6;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Patient Signature:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsPatientSignature.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsPatientSignature.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("EPCR", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsPatientSignatureEPCR.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsPatientSignatureEPCR.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Receiving Facility Signature:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsReceivingFacilitySignature.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsReceivingFacilitySignature.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("EPCR", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsReceivingFacilitySignatureEPCR.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsReceivingFacilitySignatureEPCR.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Crew Signature:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsCrewSignature.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsCrewSignature.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("EPCR", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsCrewSignatureEPCR.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            if (objclsMeetingAgenda.IsCrewSignatureEPCR.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(childTable15);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);


            cell = new PdfPCell(new Phrase("7. AE: Pull 5 or 10 runs (under 100 runs per month 5 runs, over 100 pull 10 runs) review patient and crew signatures, and place in the report", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTableSignatue = new PdfPTable(4);
            float[] widthsSignature = new float[] { 10, 30, 30, 30 };
            childTableSignatue.SetWidths(widthsSignature);

            cell = new PdfPCell(new Phrase("Run", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableSignatue.AddCell(cell);

            cell = new PdfPCell(new Phrase("Patient", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableSignatue.AddCell(cell);

            cell = new PdfPCell(new Phrase("Signature", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableSignatue.AddCell(cell);

            cell = new PdfPCell(new Phrase("Facility", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTableSignatue.AddCell(cell);

            for (int i = 0; i < objclsMeetingAgenda.lstclsSignature.Count; i++)
            {
                cell = new PdfPCell(new Phrase((i + 1).ToString(), fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTableSignatue.AddCell(cell);

                cell = new PdfPCell(new Phrase(objclsMeetingAgenda.lstclsSignature[i].Patient, fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTableSignatue.AddCell(cell);

                cell = new PdfPCell(new Phrase(objclsMeetingAgenda.lstclsSignature[i].Signature, fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTableSignatue.AddCell(cell);

                cell = new PdfPCell(new Phrase(objclsMeetingAgenda.lstclsSignature[i].Facility, fontContent));
                cell.PaddingBottom = 5f;
                cell.Colspan = 1;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                childTableSignatue.AddCell(cell);
            }

            cell = new PdfPCell(childTableSignatue);
            cell.Colspan = 2;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase("8. Month End Report Reconciliation Tutorial (report to run)", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MERComments, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTable16 = new PdfPTable(2);
            float[] widthsC16 = new float[] { 14, 14 };
            childTable16.SetWidths(widthsC16);

            childTable16.SplitLate = false;
            //childTable16.SplitRows = false;

            cell = new PdfPCell(new Phrase("Training Pending", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable16.AddCell(cell);

            cell = new PdfPCell(new Phrase("Training Completed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable16.AddCell(cell);

            if (objclsMeetingAgenda.IsTrainingPending.ToUpper() == "YES")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable16.AddCell(cell);

            if (objclsMeetingAgenda.IsTrainingPending.ToUpper() == "NO")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("  ", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable16.AddCell(cell);

            cell = new PdfPCell(childTable16);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTable17 = new PdfPTable(5);
            float[] widthsC17 = new float[] { 28, 20, 12, 20, 20 };
            childTable17.SetWidths(widthsC17);

            //childTable17.SplitLate = false;
            //childTable17.SplitRows = false;
            childTable17.SplitLate = false;

            cell = new PdfPCell(new Phrase("9. Client Review Intervals", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 4;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Monthly", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            if (objclsMeetingAgenda.CRI == "Monthly")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Next Review Schedule Date", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.NRScheduleDate, fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Quarterly", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            if (objclsMeetingAgenda.CRI == "Quarterly")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable17.AddCell(cell);


            cell = new PdfPCell(new Phrase("Change in ZOHO", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ChangeInZOHO, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Semi-Annual", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            if (objclsMeetingAgenda.CRI == "Semi-Annual")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable17.AddCell(cell);


            cell = new PdfPCell(new Phrase("Yearly", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            if (objclsMeetingAgenda.CRI == "Yearly")
            {
                cell = new PdfPCell(Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/tick.png")), false);
            }
            else
            {
                cell = new PdfPCell(new Phrase("", fontHeader));
            }
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable17.AddCell(cell);

            cell = new PdfPCell(childTable17);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTable18 = new PdfPTable(5);
            float[] widthsC18 = new float[] { 28, 20, 20, 12, 20 };
            childTable18.SetWidths(widthsC18);

            childTable18.SplitLate = false;
            //childTable18.SplitRows = false;

            cell = new PdfPCell(new Phrase("10. ePCR:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("Name:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("Reconciliation of runs last performed", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("DATE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ePCRDate, fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ePCRName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("By Whom", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.ePCRByWhom, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(childTable18);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);



            cell = new PdfPCell(new Phrase("11. Address Information", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            PdfPTable childTableAddr = new PdfPTable(2);
            float[] widthsAddr = new float[] { 37, 63 };
            childTableAddr.SetWidths(widthsAddr);

            childTableAddr.SplitLate = false;
            //childTableAddr.SplitRows = false;

            cell = new PdfPCell(new Phrase("Billing Street:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BillingStreet, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Billing City:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BillingCityName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Billing State:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BillingStateName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Billing Zip:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.BillingZip, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mailing Street:", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MailingStreet, fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mailing City:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MailingCityName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mailing State:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MailingStateName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mailing Zip:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.MailingZip, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);


            //PdfPTable childTablePhyAddr = new PdfPTable(2);
            //float[] widthsPhyAddr = new float[] { 37, 63 };
            //childTablePhyAddr.SetWidths(widthsPhyAddr);

            //childTablePhyAddr.SplitLate = false;
            ////childTableAddr.SplitRows = false;

            cell = new PdfPCell(new Phrase("Physical Location Street:", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PhysicalLocationStreet, fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Physical Location City:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PhysicalLocationCityName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase("Physical Location State:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PhysicalLocationStateName, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);


            cell = new PdfPCell(new Phrase("Physical Location Zip:", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.PhysicalLocationZip, fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableAddr.AddCell(cell);



            cell = new PdfPCell(childTableAddr);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTableCTD.AddCell(cell);

            cell = new PdfPCell(childTableCTD);
            //cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);












            PdfPTable childTable19 = new PdfPTable(1);
            float[] widthsC19 = new float[] { 100 };
            childTable19.SetWidths(widthsC19);

            childTable19.SplitLate = false;
            //childTable19.SplitRows = false;

            cell = new PdfPCell(new Phrase("OVERALL MEETING NOTES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable19.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.OverAllMeetingNotes, fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.MinimumHeight = 50f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable19.AddCell(cell);

            //cell = new PdfPCell(childTable19);
            ////cell.PaddingTop = 10f;
            ////cell.PaddingBottom = 10f;
            //cell.Colspan = 3;
            //cell.Border = 0;
            //cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            //table.AddCell(cell);

            //PdfPTable childTable20 = new PdfPTable(1);
            //float[] widthsC20 = new float[] { 100 };
            //childTable20.SetWidths(widthsC20);

            //childTable20.SplitLate = false;
            ////childTable20.SplitRows = false;

            cell = new PdfPCell(new Phrase("Follow Up Action:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable19.AddCell(cell);

            cell = new PdfPCell(new Phrase(objclsMeetingAgenda.FollowUpAction, fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.MinimumHeight = 50f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable19.AddCell(cell);

            cell = new PdfPCell(childTable19);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            document.Add(table);


            document.Close();
            // Close the writer instance  
            writer.Close();
            // Always close open filehandles explicity  
            fs.Close();

            byte[] bytes = File.ReadAllBytes(designationFilePath);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_LEFT, new Phrase(objclsMeetingAgenda.ClientName, fontContent), 25f, 15f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase("Page - " + i.ToString(), fontContent), 568f, 15f, 0);
                    }
                }
                bytes = stream.ToArray();
            }
            File.WriteAllBytes(designationFilePath, bytes);

            return designationFilePath;

            //Response.Redirect("frmMeetingAgendaMaster.aspx");

            //ClearTextBox();

            //Response.ContentType = "application/pdf";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(designationFilePath));
            //Response.TransmitFile(designationFilePath);
            //Response.End();


            //System.Diagnostics.Process.Start(designationFileName);
        }

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