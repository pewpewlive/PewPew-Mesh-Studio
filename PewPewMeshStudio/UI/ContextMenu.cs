using ImGuiNET;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace PewPewMeshStudio.UI;

public class ContextMenu
{
    public void Initialize()
    {
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Right))
            ImGui.OpenPopup("ContextMenu");
        TooltipMenu();

        ImGuiIOPtr IO = ImGui.GetIO();

        if (IO.KeysDown[(int)Keys.LeftShift] && IO.KeysDown[(int)Keys.A])
            ImGui.OpenPopup("CreateObjectMenu");
        CreateObjectMenu();
    }

    private void TooltipMenu()
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
        ImGui.MenuItem("Plane");
        ImGui.MenuItem("Sphere");
        ImGui.MenuItem("Cylinder");
        ImGui.MenuItem("Circle");

        ImGui.Separator();

        ImGui.MenuItem("Alpha");

        ImGui.EndPopup();
    }
}
