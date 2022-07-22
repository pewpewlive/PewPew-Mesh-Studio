using ImGuiNET;

namespace PewPewMeshStudio.UI;

public class ContextMenu
{
    public void Initialize()
    {
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Right))
            ImGui.OpenPopup("ContextMenu");

        TooltipMenu();
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

    private void DeletePOPUP()
    {
        
    }
}
