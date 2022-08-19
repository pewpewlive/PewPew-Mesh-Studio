using NLua;
using PewPewMeshStudio.UI;
using PewPewMeshStudio.Core;
using Serilog;

namespace PewPewMeshStudio.LuaAPI;

public static class Interpreter
{
    public static void SayHello()
    {
        Console.WriteLine("Hello");
    }
    private static void RunFile(string path)
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
            lua.DoFile(path);
        }
    }
    public static async void Run(string path)
    {
        try 
        { 
            await Task.Run(() => RunFile(path)); 
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(Interpreter) Error encountered");
            UI.Modals.ErrorModal.errorMessage = Ex.Message;
            UIHandler.openModals = UIHandler.OpenModals.Error;
        }
    }
}
