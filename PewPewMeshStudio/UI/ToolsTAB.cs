using ImGuiNET;
using System.Numerics;

namespace PewPewMeshStudio.UI;

public class ToolsTAB
{
    bool perspective;
    public void Initialize()
    {
        ImGui.Begin("Tools");

        ImGui.Text("View");

        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(180f / 255f, 20f / 255f, 20f / 255f, 1f));
        ImGui.Button(" X ");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(20f / 255f, 180f / 255f, 20f / 255f, 1f));
        ImGui.Button(" Y ");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(20f / 255f, 20f / 255f, 180f / 255f, 1f));
        ImGui.Button(" Z ");

        ImGui.Checkbox("Isometric", ref perspective);

        ImGui.PopStyleColor(3);
        ImGui.PopID();

        ImGui.End();
    }
}
