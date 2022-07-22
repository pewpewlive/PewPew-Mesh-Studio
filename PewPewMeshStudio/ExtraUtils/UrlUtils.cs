using System.Diagnostics;

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
            Console.WriteLine(e.Message);
        }
    }
}
