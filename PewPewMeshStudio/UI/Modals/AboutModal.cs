using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio.UI.Modals;

public class AboutModal
{
    public bool open;

    public void Initialize()
    {
        if (UIHandler.openModals == UIHandler.OpenModals.About)
            open = true;

        ImGui.OpenPopup(I18n.c.GetString("About PewPew Mesh Studio"));

        if (!ImGui.BeginPopupModal(I18n.c.GetString("About PewPew Mesh Studio"), ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }
        ImGui.Text(I18n.c.GetString("Copyright © PewPew Mesh Studio team & contributors."));
        ImGui.Text(I18n.c.GetString("Licensed under zlib license."));
        if (ImGui.Button(I18n.c.GetString("GitHub")))
            OpenURL("https://github.com/pewpewlive/PewPew-Mesh-Studio");

        ImGui.SameLine();
        if (ImGui.Button(I18n.c.GetString("Translate")))
            OpenURL("https://github.com/pewpewlive/Mesh-Studio-i18n");

        ImGui.SameLine();
        if (ImGui.Button(I18n.c.GetString("Website")))
            OpenURL("https://meshstudio.pewpew.live");

        ImGui.EndPopup();
    }

    private async Task OpenURL(string url) => await Task.Run(() => UrlUtils.OpenUrl(url));
}
