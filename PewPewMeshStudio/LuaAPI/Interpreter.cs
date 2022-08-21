using NLua;
using PewPewMeshStudio.UI;
using PewPewMeshStudio.Core;
using Serilog;
using System.Threading;

namespace PewPewMeshStudio.LuaAPI;

public static class Interpreter
{
    private static string LuaPath;
    private static void RunFile()
    {
        using (Lua lua = new Lua())
        { 
            // Create an API table
            lua.NewTable("API");

            // Register methods from API class
#pragma warning disable CS8974
            lua["API.AddLog"] = API.AddLog;
            lua["API.ShowModal"] = API.ShowModal;
            lua["API.RenderMeshFile"] = API.RenderMeshFile;
            lua["API.RenderMeshTable"] = API.RenderMeshTable;
#pragma warning restore CS8974
            Log.Information("(Interpreter) <{Thread}> Plugin API initialized", Thread.CurrentThread.Name);
            lua.DoFile(LuaPath);
        }
    }
    public static void Run(string path)
    {
        try 
        {
            //await Task.Run(() => RunFile(path)); 
            Log.Information("(Interpreter) <{Thread}> Spawning a new plugin thread...", Thread.CurrentThread.Name);
            Thread pluginThread = new Thread(new ThreadStart(RunFile));
            pluginThread.Name = "PluginThread";
            LuaPath = path;
            pluginThread.Start();
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(Interpreter) <Thread> Error encountered", Thread.CurrentThread.Name);
            UI.Modals.ErrorModal.errorMessage = Ex.Message;
            UIHandler.openModals = UIHandler.OpenModals.Error;
        }
    }
}
