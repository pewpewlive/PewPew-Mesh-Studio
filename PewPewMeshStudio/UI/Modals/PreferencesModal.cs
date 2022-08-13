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

    private string[] prefsItems = { I18n.c.GetString("Graphics"), I18n.c.GetString("Keybinds"), I18n.c.GetString("Plugins"), I18n.c.GetString("Interface"), I18n.c.GetString("Project"), I18n.c.GetString("Editor") };
    private int prefSelected;

    private string[] openglItems = { "3.3", "4.1", "4.6" };
    public int oglSelected = 1; // display 4.1 as chosen

    private string[] languageItems = { "English", "Lithuanian (Lietuviškai)", "Russian (Русский)", "Ukrainian (Українська)", "Greek (Ελληνικά)", "French (Français)" };
    public int langSelected;

    private string[] fontItems = { "Nunito (Default)", "ImGui" };
    public int fontSelected;

    private string[] themeItems = { I18n.c.GetString("Dark"), I18n.c.GetString("Light"), I18n.c.GetString("Classic") };
    public int themeSelected;

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup(I18n.c.GetString("Preferences"));

        if (!ImGui.BeginPopupModal(I18n.c.GetString("Preferences"), ref open))
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
                ImGui.Text(I18n.c.GetString("Mesh Rendering"));

                ImGui.Checkbox(I18n.c.GetString("Enable Antialiasing"), ref antiAliasOn);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip(I18n.c.GetString("Toggles mesh antialiasing in Editor."));

                ImGui.Text(I18n.c.GetString("OpenGL version"));
                ImGui.ListBox(" ", ref oglSelected, openglItems, openglItems.Length);
                /*if (ImGui.IsItemHovered())
                    ImGui.SetTooltip(I18n.c.GetString("Choose an OpenGL version."));*/
                return;

            case 1:
                ImGui.Text(I18n.c.GetString("Keybinds"));
                return;

            case 2:
                ImGui.Text(I18n.c.GetString("Plugins list"));
                return;

            case 3:
                // Multiple ListBoxes in the same case require different labels. Remember that and put multiple spaces.
                ImGui.Text(I18n.c.GetString("Language"));
                ImGui.ListBox(" ", ref langSelected, languageItems, languageItems.Length);

                ImGui.NewLine();

                ImGui.Text(I18n.c.GetString("Font"));

                ImGui.ListBox("  ", ref fontSelected, fontItems, fontItems.Length);

                ImGui.NewLine();

                ImGui.Text(I18n.c.GetString("Theme"));
                ImGui.ListBox("   ", ref themeSelected, themeItems, themeItems.Length);
                ChangeTheme(themeSelected);

                return;
            case 4:
                ImGui.Text(I18n.c.GetString("Undo / Redo behavior"));
                ImGui.Checkbox(I18n.c.GetString("Display Latest Action"), ref displayLastAction);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip(I18n.c.GetString("Displays last action you've done at the bottom of the Editor window."));

                return;
            case 5:
                ImGui.Text(I18n.c.GetString("Debug features"));
                ImGui.Checkbox(I18n.c.GetString("Display debug console"), ref displayDebugConsole);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip(I18n.c.GetString("Displays debug console."));
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
