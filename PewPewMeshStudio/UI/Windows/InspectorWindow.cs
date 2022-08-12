using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI.Windows;

public class InspectorWindow
{
    //test fields
    Vector3 objectPos = new Vector3(1, 2, 3);
    Vector3 meshPos = new Vector3(3, 5, 6);

    Vector3 vertexPos = new Vector3(2, 15, 63);
    Vector4 vertexCol = ColorUtil.Vec4ByteToFloat(new Vector4(2, 15, 63, 255));

    int meshesIndex = 0;
    int vertexesIndex = 0;

    public void Initialize()
    {
        ImGui.Begin("Inspector");

        ImGui.Text("Object: *selected object name*");
        ImGui.DragFloat3("Object Position", ref objectPos);

        ImGui.Text("\nMeshes");
        ImGui.ListBox("", ref meshesIndex, new string[] { "1", "2", "3" }, 3);

        ImGui.DragFloat3("Mesh Position", ref meshPos);

        ImGui.Separator();

        ImGui.Text("Vertex");
        ImGui.DragFloat3("Position", ref vertexPos);

        ImGui.NewLine();

        ImGui.ColorEdit4("Color", ref vertexCol, ImGuiColorEditFlags.AlphaPreview);

        ImGui.End();
    }
}
