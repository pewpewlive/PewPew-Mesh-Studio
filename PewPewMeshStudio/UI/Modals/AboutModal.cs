using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Threading;

namespace PewPewMeshStudio.UI.Modals;

public class AboutModal
{
    public bool open;

    public void Initialize()
    {
        if (UIHandler.openModals == UIHandler.OpenModals.About)
            open = true;

        ImGui.OpenPopup("About PewPew Mesh Studio");

        if (!ImGui.BeginPopupModal("About PewPew Mesh Studio", ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }

        ImGui.Text("Copyright © PewPew Mesh Studio team & contributors.");
        ImGui.Text("Licensed under zlib license.");

        if (ImGui.Button("GitHub"))
            OpenURL("https://github.com/pewpewlive/PewPew-Mesh-Studio");

        ImGui.SameLine();
        if (ImGui.Button("Translate"))
            OpenURL("https://github.com/pewpewlive/Mesh-Studio-i18n");

        ImGui.SameLine();
        if (ImGui.Button("Website"))
            OpenURL("https://meshstudio.pewpew.live");

        ImGui.EndPopup();
    }

    private async Task OpenURL(string url) => await Task.Run(() => UrlUtils.OpenUrl(url));
}
