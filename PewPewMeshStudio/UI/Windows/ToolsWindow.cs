using ImGuiNET;
using System.Numerics;

namespace PewPewMeshStudio.UI.Windows;

public class ToolsWindow
{
    bool perspective;
    public void Initialize()
    {
        ImGui.Begin("Tools");

        ImGui.Text("View");

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

        ImGui.Checkbox("Isometric", ref perspective);

        ImGui.PopStyleColor(3);

        ImGui.PopID();

        ImGui.End();
    }
}
