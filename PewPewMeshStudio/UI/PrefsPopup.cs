using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio.UI;

public class PrefsPopup
{
    public bool open;
    private String[] tableItems = {"English","Lithuanian","Russian","Ukrainian","Greek"};
    private int selected;
    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup("Preferences");

        if (!ImGui.BeginPopupModal("Preferences", ref open))
        {
            open1 = false;
            return;
        }
        ImGui.Text("Language:");
        ImGui.ListBox("", ref selected, tableItems, 5);
        ImGui.EndPopup();
    }
}
