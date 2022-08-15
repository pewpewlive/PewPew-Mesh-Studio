using ImGuiNET;
using OpenTK.Windowing.GraphicsLibraryFramework;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio.UI.Popups;

public class ContextMenu
{
    public void Initialize()
    {
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Right))
            ImGui.OpenPopup("ContextMenu");
        ctxMenu();

        ImGuiIOPtr IO = ImGui.GetIO();

        if (InputSystem.HotkeyPressed(new Keys[] { Keys.LeftShift, Keys.A }))
            ImGui.OpenPopup("CreateObjectMenu");
        CreateObjectMenu();
    }

    private void ctxMenu()
    {
        if (!ImGui.BeginPopup("ContextMenu"))
            return;

        ImGui.MenuItem(I18n.c.GetString("Deselect"), "X");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Deselects your selection."));

        ImGui.Separator();

        ImGui.MenuItem(I18n.c.GetString("Copy"), "Ctrl+C");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Copies selection to clipboard."));
        ImGui.MenuItem(I18n.c.GetString("Paste"), "Ctrl+V");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Pastes an item from clipboard."));

        ImGui.Separator();

        ImGui.MenuItem(I18n.c.GetString("Subdivide"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Subdivides the selected vertices."));

        ImGui.Separator();

        ImGui.MenuItem(I18n.c.GetString("Delete"), "Del");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Deletes selected items."));

        ImGui.EndPopup();
    }

    private void CreateObjectMenu()
    {
        if (!ImGui.BeginPopup("CreateObjectMenu"))
            return;

        ImGui.MenuItem(I18n.c.GetString("Cube"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Creates a cube."));
        ImGui.MenuItem(I18n.c.GetString("Rectangle"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Creates a mesh with 4 vertices."));
        ImGui.MenuItem(I18n.c.GetString("UV Sphere"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Creates a UV sphere."));
        ImGui.MenuItem(I18n.c.GetString("Ico Sphere"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Creates an icosphere."));
        ImGui.MenuItem(I18n.c.GetString("Cylinder"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Creates a cylinder."));
        ImGui.MenuItem(I18n.c.GetString("Circle"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Creates a 2D circle."));

        ImGui.Separator();

        ImGui.MenuItem(I18n.c.GetString("Alpha"));
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Construct Alpha ship mesh."));

        ImGui.EndPopup();
    }
}
