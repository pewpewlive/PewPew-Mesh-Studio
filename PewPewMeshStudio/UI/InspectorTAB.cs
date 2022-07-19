using ImGuiNET;
using PewPewMeshStudio.Core;

namespace PewPewMeshStudio.UI;

public class InspectorTAB
{
    ImGuiController UIController;
    
    public void InitializeTab()
    {
        ImGui.Begin("Inspector");

        ImGui.ShowDemoWindow();

        ImGui.End();
    }
}
