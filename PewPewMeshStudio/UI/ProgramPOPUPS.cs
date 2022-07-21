using ImGuiNET;

namespace PewPewMeshStudio.UI;

public class ProgramPOPUPS
{
    

    public void Initialize()
    {
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Right))
            ImGui.OpenPopup("geometry_tools");

        TooltipMENU();
    }

    private void TooltipMENU()
    {
        if (!ImGui.BeginPopup("geometry_tools"))
            return;

        ImGui.Selectable("Subdivide");

        ImGui.EndPopup();
    }

    private void DeletePOPUP()
    {
        
    }
}
