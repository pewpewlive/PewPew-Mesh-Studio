using ImGuiNET;

namespace PewPewMeshStudio.UI;

public class ErrorPopup
{
    // NOT COMPLETE
    public static void Initialize()
    {
        ImGui.OpenPopup("Error encountered");

        ImGui.BeginPopupModal("Error encountered");
        ImGui.Text("Error: *error string here*");

        ImGui.EndPopup();
    }
}
