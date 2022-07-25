using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio.UI.Modals;

public class AboutModal
{
    public bool open;
    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup("About PewPew Mesh Studio");

        if (!ImGui.BeginPopupModal("About PewPew Mesh Studio", ref open))
        {
            open1 = false;
            return;
        }
        ImGui.Text("Copyright © PewPew Mesh Studio team & contributors.");
        ImGui.Text("Licensed under zlib license.");
        if (ImGui.Button("GitHub"))
        {
            UrlUtils.OpenUrl("https://github.com/pewpewlive/PewPew-Mesh-Studio");
        }
        ImGui.SameLine();
        if (ImGui.Button("Website"))
        {
            UrlUtils.OpenUrl("https://meshstudio.pewpew.live");
        }
        ImGui.EndPopup();
    }
}
