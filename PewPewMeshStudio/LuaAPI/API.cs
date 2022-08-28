using Serilog;
using NLua;
using PewPewMeshStudio.UI.Modals;
using PewPewMeshStudio.UI;
using PewPewMeshStudio.Core;
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;

namespace PewPewMeshStudio.LuaAPI;

/// <summary>
/// Exposed C# API to Lua plugins.
/// </summary>
public static class API
{
    //public static Window WindowClass { set; private get; }
    /// <summary>
    /// Add a log message.
    /// </summary>
    public static void AddLog(string str)
    {
        Log.Information("(API @ AddLog) <{thread}> Plugin: {plugin_message}", Thread.CurrentThread.Name, str);
    }
    /// <summary>
    /// Shows a customizable modal.
    /// </summary>
    /// <param name="content"><i>table</i> { <i>string</i> <c>title</c>, <i>string</i> <c>description</c> }</param>
    public static void ShowModal(LuaTable content)
    {
        CustomizableModal.Title = (string?)content["title"];
        CustomizableModal.Description = (string?)content["description"];

        UIHandler.openModals = UIHandler.OpenModals.Custom;
    }
    /// <summary>
    /// Parses and renders a mesh file.
    /// </summary>
    /// <inheritdoc cref="MeshParser.ParseMeshFile(string, int)"/>
    /// <param name="path">A string containing a path.</param>
    /// <param name="index">A meshes table index.</param>
    public static void RenderMeshFile(string path, int index)
    {
        Window.requestedMeshPath = path;
        Window.requestedMeshIndex = index;
        Window.isMeshChangeRequest = true;
    }
    /// <summary>
    /// Parses and renders a mesh table.
    /// </summary>
    /// <param name="table">Lua table containing mesh data.</param>
    /// <param name="index">A meshes table index.</param>
    public static void RenderMeshTable(LuaTable table, int index)
    {
        throw new NotImplementedException();
    }
}
