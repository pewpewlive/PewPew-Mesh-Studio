using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI;

public class PreferencesPopup
{
    public bool open;
    public bool antiAliasOn;
    public bool displayLastAction;
    // public bool blackBg;

    private String lastActionDone = "Not Applicable";

    private string[] prefsItems = { "Project", "Keybinds", "Plugins", "Interface" };
    private int prefSelected;

    private string[] languageItems = { "English", "Lithuanian", "Russian", "Ukrainian", "Greek", "French" };
    public int langSelected;

    private string[] fontItems = { "Nunito (Default)", "ImGui" };
    public int fontSelected;

    private string[] themeItems = { "Dark", "Light", "Classic" };
    public int themeSelected;

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
                ImGui.Text("Mesh Rendering"); 

                ImGui.Checkbox("Enable Antialiasing", ref antiAliasOn);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip("Toggles mesh antialiasing in Editor.");

                /*
                ImGui.Checkbox("Display Latest Action", ref displayLastAction);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip("Displays last action you've done at the bottom of the Editor window.");
                */

                //ImGui.Checkbox("Toggle Black Background", ref blackBg); 
                return;

            case 1:
                ImGui.Text("Keybinds");
                return;

            case 2:
                ImGui.Text("Plugins list");
                return;

            case 3:
                // Multiple ListBoxes in the same case require different labels. Remember that and put multiple spaces.
                ImGui.Text("Language");
                ImGui.ListBox(" ", ref langSelected, languageItems, languageItems.Length);

                ImGui.NewLine();

                ImGui.Text("Font");
       
                ImGui.ListBox("  ", ref fontSelected, fontItems, fontItems.Length);

                ImGui.NewLine();

                ImGui.Text("Theme");
                ImGui.ListBox("   ", ref themeSelected, themeItems, themeItems.Length);
                ChangeTheme(themeSelected);

                return;

            default: // if some shit happens
                Console.WriteLine("Invalid preference list item index");
                return;
        }

    }

    private void ChangeTheme(int themeId)
    {
        switch (themeId)
        {
            case 0:
                ImGui.StyleColorsDark();
                return;

            case 1:
                ImGui.StyleColorsLight();
                return;

            case 2:
                ImGui.StyleColorsClassic();
                return;

            default:
                ImGui.StyleColorsDark();
                return;
        }
    }
}
