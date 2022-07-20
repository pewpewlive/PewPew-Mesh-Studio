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

        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(50, 0, 0, 255));
        ImGui.Button("X");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0, 1, 0, 255));
        ImGui.Button("Y");

        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0, 0, 50, 255));
        ImGui.Button("X");

        ImGui.Checkbox("Isometric", ref perspective);

        ImGui.PopStyleColor(3);
        ImGui.PopID();

        ImGui.End();
    }
}
