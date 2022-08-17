using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI.Windows;

public class InspectorWindow
{
    //test fields
    Vector3 objectPos = new Vector3();
    Vector3 meshPos = new Vector3();

    Vector3 vertexPos = new Vector3(2, 15, 63);
    Vector4 vertexCol = ColorUtil.Vec4ByteToFloat(new Vector4(2, 15, 63, 255));

    int meshIndex = 0;
    int vertexesIndex = 0;

    public static Action<OpenTK.Mathematics.Vector3> OnObjectPositionUpdate;
    public static Action<OpenTK.Mathematics.Vector3> OnMeshPositionUpdate;
    //public static Action<OpenTK.Mathematics.Vector3> OnVertexPositionUpdate;

    public void Initialize()
    {
        ImGui.Begin(I18n.c.GetString("Inspector"));

        ImGui.Text(I18n.c.GetString("Object: {0}", "mesh.lua"));
        if (ImGui.DragFloat3(I18n.c.GetString("Object Position"), ref objectPos, 0.5f))
            OnObjectPositionUpdate?.Invoke(new OpenTK.Mathematics.Vector3(objectPos.X, objectPos.Y, objectPos.Z));



        ImGui.Text(I18n.c.GetString("Meshes"));
        ImGui.BeginChild("meshList", new Vector2(0f, 125f), true, ImGuiWindowFlags.HorizontalScrollbar);

        for (int i = 0; i < Core.Window.editor.meshes.Count; i++)
        {
            ImGui.Columns(2, "yesColumnt", false);
            ImGui.SetColumnOffset(1, ImGui.GetWindowSize().X - 110f);
            if (ImGui.Selectable(i.ToString(), Core.Window.editor.meshes[i].selected))
            {
                Core.Window.editor.SetEditingMesh(i);
                OpenTK.Mathematics.Vector3 newMeshPos = Core.Window.editor.meshes[i].position;
                meshPos = new Vector3(newMeshPos.X, newMeshPos.Y, newMeshPos.Z);
            }

            ImGui.NextColumn();
            ImGui.Checkbox("Hidden" + i, ref Core.Window.editor.meshes[i].hidden);

            ImGui.NextColumn();
        }
        //ImGui.ListBox("", ref meshIndex, new string[] { "1", "2", "3" }, 3, 3);

        ImGui.EndChild();
        

        if (ImGui.DragFloat3(I18n.c.GetString("Mesh Position"), ref meshPos))
            OnMeshPositionUpdate?.Invoke(new OpenTK.Mathematics.Vector3(meshPos.X, meshPos.Y, meshPos.Z));

        ImGui.Separator();

        ImGui.Text(I18n.c.GetString("Vertex"));
        ImGui.DragFloat3(I18n.c.GetString("Position"), ref vertexPos);

        ImGui.NewLine();

        ImGui.ColorEdit4(I18n.c.GetString("Color"), ref vertexCol, ImGuiColorEditFlags.AlphaPreview);

        ImGui.End();
    }
}
