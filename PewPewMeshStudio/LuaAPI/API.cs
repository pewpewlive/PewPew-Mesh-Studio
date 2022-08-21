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
        Log.Information("(API @ AddLog) <{thread}> Plugin: {plugin_message}", Thread.CurrentThread.Name, str);
    }
    public static void ShowModal(LuaTable content)
    {
        CustomizableModal.Title = (string?)content["title"];
        CustomizableModal.Description = (string?)content["description"];

        UIHandler.openModals = UIHandler.OpenModals.Custom;
    }
    public static void RenderMeshFile(string path, int index)
    {
        Window.requestedMeshPath = path;
        Window.requestedMeshIndex = index;
        Window.isMeshChangeRequest = true;
    }
    public static void RenderMeshTable(LuaTable table, int index)
    {
        throw new NotImplementedException();
    }
}
