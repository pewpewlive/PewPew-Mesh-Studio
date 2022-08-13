using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio.UI.Modals;

public class AboutModal
{
    public bool open;
    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup(I18n.c.GetString("About PewPew Mesh Studio"));

        if (!ImGui.BeginPopupModal(I18n.c.GetString("About PewPew Mesh Studio"), ref open))
        {
            open1 = false;
            return;
        }
        ImGui.Text(I18n.c.GetString("Copyright © PewPew Mesh Studio team & contributors."));
        ImGui.Text(I18n.c.GetString("Licensed under zlib license."));
        if (ImGui.Button(I18n.c.GetString("GitHub")))
        {
            UrlUtils.OpenUrl("https://github.com/pewpewlive/PewPew-Mesh-Studio");
        }
        ImGui.SameLine();
        if (ImGui.Button(I18n.c.GetString("Website")))
        {
            UrlUtils.OpenUrl("https://meshstudio.pewpew.live");
        }
        ImGui.EndPopup();
    }
}
