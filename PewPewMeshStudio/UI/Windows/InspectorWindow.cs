using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI.Windows;

public class InspectorWindow
{
    //test fields
    Vector3 objectPos = new Vector3(0, 0, 0);
    Vector3 meshPos = new Vector3(3, 5, 6);

    Vector3 vertexPos = new Vector3(2, 15, 63);
    Vector4 vertexCol = ColorUtil.Vec4ByteToFloat(new Vector4(2, 15, 63, 255));

    int meshesIndex = 0;
    int vertexesIndex = 0;

    public static Action<OpenTK.Mathematics.Vector3> OnObjectPositionUpdate;
    //public static Action<OpenTK.Mathematics.Vector3> OnMeshPositionUpdate;
    //public static Action<OpenTK.Mathematics.Vector3> OnVertexPositionUpdate;


    public void Initialize()
    {
        ImGui.Begin(I18n.c.GetString("Inspector"));

        ImGui.Text(I18n.c.GetString("Object: {0}", "*selected object name*"));
        if (ImGui.DragFloat3(I18n.c.GetString("Object Position"), ref objectPos)) 
            OnObjectPositionUpdate?.Invoke(new OpenTK.Mathematics.Vector3(objectPos.X, objectPos.Y, objectPos.Z));

        ImGui.Text(I18n.c.GetString("Meshes"));
        ImGui.ListBox("", ref meshesIndex, new string[] { "1", "2", "3" }, 3);

        ImGui.DragFloat3(I18n.c.GetString("Mesh Position"), ref meshPos);

        ImGui.Separator();

        ImGui.Text(I18n.c.GetString("Vertex"));
        ImGui.DragFloat3(I18n.c.GetString("Position"), ref vertexPos);

        ImGui.NewLine();

        ImGui.ColorEdit4(I18n.c.GetString("Color"), ref vertexCol, ImGuiColorEditFlags.AlphaPreview);

        ImGui.End();
    }
}
