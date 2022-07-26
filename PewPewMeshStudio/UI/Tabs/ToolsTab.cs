using ImGuiNET;
using System.Numerics;

namespace PewPewMeshStudio.UI;

public class ToolsTab
{
    bool perspective;
    public void Initialize()
    {
        ImGui.Begin("Tools");

        ImGui.Text("View");

        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(180, 20, 20, 255)));
        ImGui.Button(" X ");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(20, 180, 20, 255)));
        ImGui.Button(" Y ");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(20, 20, 180, 255)));
        ImGui.Button(" Z ");

        ImGui.Checkbox("Isometric", ref perspective);

        ImGui.PopStyleColor(3);
        ImGui.PopID();

        ImGui.End();
    }
}
