using PewPewMeshStudio.UI;
using Serilog;
using System.Diagnostics;

namespace PewPewMeshStudio.MoonScript;

public static class MoonCompiler
{
    public static void Compile(string path)
    {
        // not implemented completely
        Process moonc = new Process();
        try
        {
            moonc.StartInfo.FileName = "moonscript\\moonc.exe";
            moonc.StartInfo.UseShellExecute = true;
            //moonc.StartInfo.WorkingDirectory = path;
            //moonc.StartInfo.Arguments = "-o C:\\Windows\\Temp\\ppms_cache\\";
            moonc.StartInfo.Arguments = $"{path} -o lua_cache\\test.lua";
            moonc.BeginErrorReadLine();
            moonc.Start();
            if (moonc.ExitCode != 0)
                throw new Exception("moonc error");
        }
        catch (Exception e)
        {
            Log.Error(e, "(MoonCompiler) Error encountered");
            UI.Modals.ErrorModal.errorMessage = e.Message;
            UIHandler.openModals = UIHandler.OpenModals.Error;
        }
    }
}
