using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
namespace PewPewMeshStudio.UI.Modals;

public class CustomizableModal
{
    public bool open;

    public static string? Title { private get; set; }
    public static string? Description { private get; set; }

    public void Initialize()
    {
        if (UIHandler.openModals == UIHandler.OpenModals.Custom)
            open = true;

        ImGui.OpenPopup(Title);

        if (!ImGui.BeginPopupModal(Title, ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }
        ImGui.Text(I18n.c.GetString(Description));
        

        ImGui.EndPopup();
    }
}
