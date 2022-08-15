using ImGuiNET;

namespace PewPewMeshStudio.UI.Modals;

public class ErrorModal
{
    public bool open;

    public string errorMessage = "ExampleError";

    public void Initialize()
    {
        if (UIHandler.openModals == UIHandler.OpenModals.Error)
            open = true;

        ImGui.OpenPopup("Error encountered");

        if (!ImGui.BeginPopupModal("Error encountered", ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }

        ImGui.Text("Error: " + errorMessage);

        if (ImGui.Button("Exit application"))
        {
            Environment.Exit(1); // send an error exit code
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Exits the application with a non-zero exit code.");

        ImGui.SameLine();

        if (ImGui.Button("Continue"))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Continue running the application.");
        ImGui.EndPopup();
    }
}
