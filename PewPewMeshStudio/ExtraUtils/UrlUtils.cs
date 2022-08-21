using System.Diagnostics;
using PewPewMeshStudio.UI;
using Serilog;

namespace PewPewMeshStudio.ExtraUtils;

public class UrlUtils
{
    public static void OpenUrl(string url)
    {
        Process urlProcess = new Process();
        try
        {
            // true is the default, but it is important not to set it to false
            urlProcess.StartInfo.UseShellExecute = true;
            urlProcess.StartInfo.FileName = url;
            urlProcess.Start();
        }
        catch (Exception e)
        {
            Log.Error(e, "(UrlUtils @ OpenUrl) <{thread}> Error encountered", Thread.CurrentThread.Name);
            UI.Modals.ErrorModal.errorMessage = e.Message;
            UIHandler.openModals = UIHandler.OpenModals.Error;
        }
    }
}
