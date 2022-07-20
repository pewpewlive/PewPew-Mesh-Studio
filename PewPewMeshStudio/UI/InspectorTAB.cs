using ImGuiNET;
using PewPewMeshStudio.Core;
using System.Numerics;

namespace PewPewMeshStudio.UI;

public class InspectorTAB
{
    //test fields
    Vector3 objectPos = new(1, 2, 3);
    Vector3 meshPos = new(3, 5, 6);

    Vector3 vertexPos = new(2, 15, 63);
    Vector4 vertexCol = new(2, 15, 63, 255);

    int meshesIndex = 0;
    int vertexesIndex = 0;
    public void Initialize()
    {
        ImGui.Begin("Inspector");

        if (ImGui.CollapsingHeader("Object: *selected object name*"))
        {
            ImGui.InputFloat3("Object Position", ref objectPos);

            ImGui.Text("\nMeshes");
            ImGui.ListBox("Meshes", ref meshesIndex, new string[] { "yes", "xd", "e" }, 3);

            ImGui.InputFloat3("Mesh Position", ref meshPos);

            ImGui.Separator();

            ImGui.Text("Vertex");
            ImGui.InputFloat3("Position", ref vertexPos);

            ImGui.Text("");

            ImGui.ColorEdit4("Color", ref vertexCol);
        }

        ImGui.ShowDemoWindow();

        ImGui.End();
    }
}
