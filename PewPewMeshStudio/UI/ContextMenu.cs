using ImGuiNET;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace PewPewMeshStudio.UI;

public class ContextMenu
{
    public void Initialize()
    {
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Right))
            ImGui.OpenPopup("ContextMenu");
        ctxMenu();

        ImGuiIOPtr IO = ImGui.GetIO();

        if (ImGui.IsMouseClicked(ImGuiMouseButton.Right) && IO.KeysDown[(int)Keys.LeftShift])
            ImGui.OpenPopup("CreateObjectMenu");
        CreateObjectMenu();
    }

    private void ctxMenu()
    {
        if (!ImGui.BeginPopup("ContextMenu"))
            return;

        ImGui.MenuItem("Deselect", "X");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Deselects your selection.");

        ImGui.Separator();

        ImGui.MenuItem("Copy", "Ctrl+C");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Copies selection to clipboard.");
        ImGui.MenuItem("Paste", "Ctrl+V");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Pastes an item from clipboard.");

        ImGui.Separator();

        ImGui.MenuItem("Subdivide");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Subdivides the selected vertices.");

        ImGui.Separator();

        ImGui.MenuItem("Delete", "Del");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Deletes selected items.");

        ImGui.EndPopup();
    }

    private void CreateObjectMenu()
    {
        if (!ImGui.BeginPopup("CreateObjectMenu"))
            return;

        ImGui.MenuItem("Cube");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Creates a cube.");
        ImGui.MenuItem("Rect");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Creates a mesh with 4 vertices.");
        ImGui.MenuItem("UV Sphere");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Creates a UV sphere.");
        ImGui.MenuItem("Ico Sphere");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Creates an icosphere.");
        ImGui.MenuItem("Cylinder");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Creates a cylinder.");
        ImGui.MenuItem("Circle");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Creates a 2D circle.");

        ImGui.Separator();

        ImGui.MenuItem("Alpha");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Construct Alpha ship mesh.");

        ImGui.EndPopup();
    }
}
