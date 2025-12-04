using ClientMeetingAgenda.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsInitialize objclsInitialize = new clsInitialize();
            clsRecords objclsRecords = new clsRecords();

            objclsInitialize.SDKInitialize();
            string recordID = objclsRecords.SearchRecords();

        }
    }
}