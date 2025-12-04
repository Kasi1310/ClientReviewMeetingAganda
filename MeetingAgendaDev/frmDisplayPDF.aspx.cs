using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
    public partial class frmDisplayPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PrintDocument"] != null)
            {
                string destinationPDFFilePath = Session["PrintDocument"].ToString().Trim();

                Session["PrintDocument"] = null;

                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(destinationPDFFilePath);


                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.Flush();
                    Response.Close();
                    Response.End();
                }

                if(File.Exists(destinationPDFFilePath))
                {
                    File.Delete(destinationPDFFilePath);
                }

            }
        }
    }
}