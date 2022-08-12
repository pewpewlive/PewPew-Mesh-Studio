using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI.Windows;

public class ToolsWindow
{
    bool perspective;
    public void Initialize()
    {
        ImGui.Begin(I18n.c.GetString("Tools"));

        ImGui.Text(I18n.c.GetString("View"));

        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(180, 20, 20, 255)));
        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(8f, 3f));
        ImGui.Button("X");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(20, 180, 20, 255)));
        ImGui.Button("Y");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(20, 20, 180, 255)));
        ImGui.Button("Z");

        ImGui.PopStyleVar();

        ImGui.Checkbox(I18n.c.GetString("Isometric"), ref perspective);

        ImGui.PopStyleColor(3);

        ImGui.PopID();

        ImGui.End();
    }
}
