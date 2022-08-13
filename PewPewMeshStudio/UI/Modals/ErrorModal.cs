using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio.UI.Modals;

public class ErrorModal
{
    public bool open;

    public string errorMessage = "ExampleError";

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup(I18n.c.GetString("Error encountered"));

        if (!ImGui.BeginPopupModal(I18n.c.GetString("Error encountered"), ref open))
        {
            open1 = false;
            return;
        }

        ImGui.Text(I18n.c.GetString("Error: ") + errorMessage);

        if (ImGui.Button(I18n.c.GetString("Quit")))
        {
            Environment.Exit(1); // send an error exit code
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Exits the application with a non-zero exit code."));

        ImGui.SameLine();

        if (ImGui.Button(I18n.c.GetString("Continue")))
        {
            open1 = false;
            return;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Continue running the application."));
        ImGui.EndPopup();
    }
}
