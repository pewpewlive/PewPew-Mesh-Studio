using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio.UI.Modals;

public class ErrorModal
{
    public bool open;

    public static string errorMessage { private get; set; }

    public void Initialize()
    {
        if (UIHandler.openModals == UIHandler.OpenModals.Error)
            open = true;

        ImGui.OpenPopup(I18n.c.GetString("Error encountered"));

        if (!ImGui.BeginPopupModal(I18n.c.GetString("Error encountered"), ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
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
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Continue running the application."));
        ImGui.EndPopup();
    }
}
