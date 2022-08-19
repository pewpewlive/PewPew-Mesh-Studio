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
            lua["API.SetMeshFile"] = API.SetMeshFile;
            lua["API.SetMeshTable"] = API.SetMeshTable;
#pragma warning restore CS8974
            lua.DoFile(LuaPath);
        }
    }
    public static void Run(string path)
    {
        try 
        {
            //await Task.Run(() => RunFile(path)); 
            Log.Information("(Interpreter) Spawning a new plugin thread...");
            Thread pluginThread = new Thread(new ThreadStart(RunFile));
            LuaPath = path;
            pluginThread.Start();
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(Interpreter) Error encountered");
            UI.Modals.ErrorModal.errorMessage = Ex.Message;
            UIHandler.openModals = UIHandler.OpenModals.Error;
        }
    }
}
