using PewPewMeshStudio.Core;
using PewPewMeshStudio.LuaUtils;
using PewPewMeshStudio.LuaAPI;
using PewPewMeshStudio.ExtraUtils;
using Serilog;
using System.Threading;

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
            //.WriteTo.File("logs/.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("(Program) <{0}> Welcome to PewPew Mesh Studio v0.1-Unstable", Thread.CurrentThread.Name);
        
        Window MainWindow = new Window();
        unsafe { CursorSetter.WindowPointer = MainWindow.WindowPtr; }
        MainWindow.Run();
        Log.CloseAndFlush();
    }
}