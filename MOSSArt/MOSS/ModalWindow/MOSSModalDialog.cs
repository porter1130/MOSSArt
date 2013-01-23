using System.Web.UI;

namespace MOSSArt
{
    public class MOSSModalDialog
    {
        public static void Close(Page page)
        {
            if (page.Request["IsDlg"] == "1")
            {
                page.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                page.Response.Flush();
                page.Response.End();
            }
        }

        public static void CloseWithAlert(Page page, string msg)
        {
            if (page.Request["IsDlg"] == "1")
            {
                page.Response.Write(string.Format("<script type='text/javascript'>window.frameElement.commitPopup();alert('{0}')</script>", msg));
                page.Response.Flush();
                page.Response.End();
            }
        }
    }
}
