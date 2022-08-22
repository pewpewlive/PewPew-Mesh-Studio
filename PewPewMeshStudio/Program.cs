using PewPewMeshStudio.Core;
using PewPewMeshStudio.LuaUtils;
using PewPewMeshStudio.LuaAPI;
using PewPewMeshStudio.ExtraUtils;
using PewPewMeshStudio.Preferences;
using Serilog;
using System.Threading;
using PewPewMeshStudio.PPMP;

namespace PewPewMeshStudio;

class Program
{
    static void Main()
    {
        //ConsoleExtension.Hide();
        //ConsoleExtension.Show();
        Thread.CurrentThread.Name = "MainThread";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            //.WriteTo.Async(asyncWrite => asyncWrite.File("logs/.log", rollingInterval: RollingInterval.Day), blockWhenFull: true)
            .CreateLogger();

        Log.Information("(Program @ Main) <{thread}> Welcome to PewPew Mesh Studio v0.1-Unstable", Thread.CurrentThread.Name);

        /*MeshProjectIO.Save("test.ppmp", new MeshProject()
        {
            PpmpRevision = 1,
            CurrentMeshes = new string[] { "s.lua", "mesh.lua" },
            CurrentMeshesIndexes = new int[] { 1, 1 },
            CurrentPos = new System.Numerics.Vector3(3f, 5f, 6.5f)
        });*/

        //MeshProjectIO.Load("test.ppmp");

        /*PrefIO.Save("prefs.json", new Prefs()
        {
            PrefsRevision = 1,
            Language = "en",
            Font = "internal",
            Scale = 1.0f,
            OpenGlVersion = new Version(4, 1),
            Theme = "dark",
            ShowLastAction = false,
            ShowConsole = true
        });*/
        //PrefIO.Load("prefs.json");

        Window MainWindow = new Window();
        unsafe { CursorSetter.WindowPointer = MainWindow.WindowPtr; }
        MainWindow.Run();
        Log.CloseAndFlush();
    }
}