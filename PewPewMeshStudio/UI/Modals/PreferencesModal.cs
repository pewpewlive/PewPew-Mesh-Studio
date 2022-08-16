using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;
using Serilog;

namespace PewPewMeshStudio.UI.Modals;

public class PreferencesModal
{
    public bool open;
    public bool antiAliasOn;
    public bool displayLastAction;
    public bool displayDebugConsole;
    // public bool blackBg;

    private string lastActionDone = "Not Applicable";

    private string[] prefsItems = { "Graphics", "Keybinds", "Plugins", "Interface", "Project", "Editor" };
    private int prefSelected;

    private string[] openglItems = { "3.3", "4.1", "4.6" };
    public int oglSelected = 1; // display 4.1 as chosen

    private string[] languageItems = { "English", "Lithuanian (Lietuviškai)", "Russian (Русский)", "Ukrainian (Українська)", "Greek (Ελληνικά)", "French (Français)" };
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

                ImGui.Text("OpenGL version");
                ImGui.ListBox(" ", ref oglSelected, openglItems, openglItems.Length);
                /*if (ImGui.IsItemHovered())
                    ImGui.SetTooltip("Choose an OpenGL version.");*/
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
            case 4:
                ImGui.Text("Undo / Redo behavior");
                ImGui.Checkbox("Display Latest Action", ref displayLastAction);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip("Displays last action you've done at the bottom of the Editor window.");

                return;
            case 5:
                ImGui.Text("Debug features");
                ImGui.Checkbox("Display debug console", ref displayDebugConsole);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip("Displays debug console.");
                return;
            default: // if some shit happens
                Log.Error("Invalid preference list item index.");
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
