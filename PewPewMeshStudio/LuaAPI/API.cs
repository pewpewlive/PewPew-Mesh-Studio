using Serilog;
using NLua;
using PewPewMeshStudio.UI.Modals;
using PewPewMeshStudio.UI;
using PewPewMeshStudio.Core;
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;

namespace PewPewMeshStudio.LuaAPI;

public static class API
{
    //public static Window WindowClass { set; private get; }
    public static void AddLog(string str)
    {
        Log.Information("(API) <{0}> {1}", Thread.CurrentThread.Name, str);
    }
    public static void ShowModal(LuaTable content)
    {
        CustomizableModal.Title = (string?)content["title"];
        CustomizableModal.Description = (string?)content["description"];

        UIHandler.openModals = UIHandler.OpenModals.Custom;
    }
    public static void SetMeshFile(string path, int index)
    {
        throw new NotImplementedException();
    }
    public static void SetMeshTable(LuaTable table, int index)
    {
        throw new NotImplementedException();
    }
}
