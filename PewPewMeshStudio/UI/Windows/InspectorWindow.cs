using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;
using OpenTK.Windowing.GraphicsLibraryFramework;

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

    bool openPopup = false; //cringe
    int hoveredMesh = 0;

    public void Initialize()
    {
        ImGui.Begin(I18n.c.GetString("Inspector"));

        ImGui.Text(I18n.c.GetString("Object: {0}", "mesh.lua"));
        if (ImGui.DragFloat3(I18n.c.GetString("Object Position"), ref objectPos, 0.5f))
            OnObjectPositionUpdate?.Invoke(new OpenTK.Mathematics.Vector3(objectPos.X, objectPos.Y, objectPos.Z));
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(CursorShape.HResize);

        InitMeshListChild();
        
        if (openPopup)
        {
            openPopup = false;
            ImGui.OpenPopup("MeshCxtPopup");
        }
        MeshContextMenuPopup();

        ImGui.Text(I18n.c.GetString("Vertex"));
        ImGui.DragFloat3(I18n.c.GetString("Position"), ref vertexPos);
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(CursorShape.HResize);

        ImGui.NewLine();

        ImGui.ColorEdit4(I18n.c.GetString("Color"), ref vertexCol, ImGuiColorEditFlags.AlphaPreview);
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(CursorShape.HResize);

        ImGui.End();
    }

    private void InitMeshListChild()
    {
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
            if (ImGui.IsItemHovered() && InputSystem.KeyPressed(Keys.Q))
            {
                openPopup = true;
                hoveredMesh = i;
            }

            ImGui.NextColumn();
            ImGui.Checkbox("Hidden" + i, ref Core.Window.editor.meshes[i].hidden);
            ImGui.NextColumn();
        }
        ImGui.EndChild();
        ImGui.Separator();

        if (ImGui.DragFloat3(I18n.c.GetString("Mesh Position"), ref meshPos))
            OnMeshPositionUpdate?.Invoke(new OpenTK.Mathematics.Vector3(meshPos.X, meshPos.Y, meshPos.Z));
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(CursorShape.HResize);
    }

    private void MeshContextMenuPopup()
    {
        if (!ImGui.BeginPopup("MeshCxtPopup")) //lol
            return;

        ImGui.MenuItem("Rename");
        ImGui.MenuItem("Copy", "Ctrl + C");
        //ImGui.MenuItem("Duplicate", "Ctrl + D");
        if (ImGui.MenuItem("Delete", "X"))
            Core.Window.editor.DeleteMesh(hoveredMesh);

        ImGui.EndPopup();
    }
}
