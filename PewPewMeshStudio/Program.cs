using PewPewMeshStudio.Core;
using PewPewMeshStudio.LuaUtils;
using PewPewMeshStudio.LuaAPI;
using PewPewMeshStudio.ExtraUtils;
using Serilog;

namespace PewPewMeshStudio;

class Program
{
    static void Main()
    {
        //ConsoleExtension.Hide();
        //ConsoleExtension.Show();
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            //.WriteTo.File("logs/.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Window MainWindow = new Window();
        unsafe { CursorSetter.WindowPointer = MainWindow.WindowPtr; }
        MainWindow.Run();
        Log.Information("(Program) Closed GUI, closing application...");
        Log.CloseAndFlush();
    }
}