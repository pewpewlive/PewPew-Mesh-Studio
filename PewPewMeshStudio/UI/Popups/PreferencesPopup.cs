using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI;

public class PreferencesPopup
{
    public bool open;

    private string[] prefsItems = { "Project", "Keybinds", "Plugins", "Interface" };
    private int prefSelected;

    private string[] languageItems = { "English", "Lithuanian", "Russian", "Ukrainian", "Greek" };
    public int langSelected;

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup("Preferences");

        if (!ImGui.BeginPopupModal("Preferences", ref open))
        {
            open1 = false;
            return;
        }

        ImGui.Columns(2, "SettingsListColumn", false);
        ImGui.SetColumnWidth(0, 100f);

        ImGui.SetNextItemWidth(ImGui.GetColumnWidth(0));
        ImGui.ListBox("", ref prefSelected, prefsItems, prefsItems.Length);

        ImGui.NextColumn();

        PreferenceItemEdit();

        ImGui.EndPopup();
    }

    private void PreferenceItemEdit()
    {
        switch (prefSelected)
        {
            case 0:
                ImGui.Text("You can do here something lol");
                return;

            case 1:
                ImGui.Text("Keybinds");
                return;

            case 2:
                ImGui.Text("Plugins list");
                return;

            case 3:
                ImGui.Text("Language");
                ImGui.ListBox(" ", ref langSelected, languageItems, languageItems.Length);

                ImGui.NewLine();

                ImGui.Text("Font");
                return;

            default: // if some shit happens
                Console.WriteLine("Invalid preference list item index");
                return;
        }
    }
}
