﻿using ImGuiNET;
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
        ImGui.Begin(I18n.c.GetString("Inspector"));

        ImGui.Text(I18n.c.GetString("Object: {0}", "*selected object name*"));
        ImGui.DragFloat3(I18n.c.GetString("Object Position"), ref objectPos);
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(OpenTK.Windowing.GraphicsLibraryFramework.CursorShape.HResize);

        ImGui.Text(I18n.c.GetString("Meshes"));
        ImGui.ListBox("", ref meshesIndex, new string[] { "1", "2", "3" }, 3);

        ImGui.DragFloat3(I18n.c.GetString("Mesh Position"), ref meshPos);
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(OpenTK.Windowing.GraphicsLibraryFramework.CursorShape.HResize);

        ImGui.Separator();

        ImGui.Text(I18n.c.GetString("Vertex"));
        ImGui.DragFloat3(I18n.c.GetString("Position"), ref vertexPos);
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(OpenTK.Windowing.GraphicsLibraryFramework.CursorShape.HResize);

        ImGui.NewLine();

        ImGui.ColorEdit4(I18n.c.GetString("Color"), ref vertexCol, ImGuiColorEditFlags.AlphaPreview);
        if (ImGui.IsItemHovered())
            CursorSetter.SetCursor(OpenTK.Windowing.GraphicsLibraryFramework.CursorShape.HResize);

        ImGui.End();
    }
}
