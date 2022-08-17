using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI.Modals;

public class SplashScreen
{
    public bool open;
    public void Initialize()
    {
        if (UIHandler.openModals == UIHandler.OpenModals.SplashScreen)
            open = true;

        ImGui.OpenPopup(I18n.c.GetString("Welcome to PewPew Mesh Studio!"));

        if (!ImGui.BeginPopupModal(I18n.c.GetString("Welcome to PewPew Mesh Studio!"), ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }
        
        ImGui.Text(I18n.c.GetString("Welcome to PewPew Mesh Studio"));

        ImGui.EndPopup();
    }
}
