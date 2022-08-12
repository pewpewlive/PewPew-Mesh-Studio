using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System;

namespace PewPewMeshStudio.UI;

public class GlobalMenu
{
    public bool OpenErrorDialog;
    public bool OpenAboutDialog;
    public bool OpenFileDialog;
    public bool OpenPrefsDialog;
    public int fileDialogType;

    public void Initialize()
    {
        ImGui.BeginMainMenuBar();

        FileMenu();
        EditMenu();
        DebugMenu();

        ImGui.EndMainMenuBar();
    }

    private void FileMenu()
    {
        if (!ImGui.BeginMenu(I18n.c.GetString("File")))
            return;

        if (ImGui.MenuItem(I18n.c.GetString("New")))
        {
            OpenFileDialog = true;
            fileDialogType = 0;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Create a new project."));

        if (ImGui.MenuItem(I18n.c.GetString("Open")))
        {
            OpenFileDialog = true;
            fileDialogType = 1;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Open a project file."));

        ImGui.MenuItem(I18n.c.GetString("Save"), "Ctrl+S"); // save
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Saves the current project file."));

        if (ImGui.MenuItem(I18n.c.GetString("Save as...")))
        {
            OpenFileDialog = true;
            fileDialogType = 2;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Saves the current project file in the desired location."));

        ImGui.Separator();

        if (ImGui.MenuItem(I18n.c.GetString("Import")))
        {
            OpenFileDialog = true;
            fileDialogType = 3;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Load a PewPew mesh or an SVG."));

        if (ImGui.MenuItem("Export"))
        {
            OpenFileDialog = true;
            fileDialogType = 4;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Export the current project as a PewPew mesh."));

        ImGui.Separator();

        if (ImGui.MenuItem(I18n.c.GetString("Quit"), "Alt+F4"))
        {
            Environment.Exit(0);
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Quit PewPew Mesh Studio."));

        ImGui.EndMenu();
    }

    private void EditMenu()
    {
        if (!ImGui.BeginMenu("Edit"))
            return;

        ImGui.MenuItem("Undo", "Ctrl+Z");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Undo previous action.");
        ImGui.MenuItem("Redo", "Ctrl+Y");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Redo previous action.");

        ImGui.Separator();

        if (ImGui.MenuItem("Preferences"))
        {
            OpenPrefsDialog = true;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Edit user preferences.");
        ImGui.Separator();

        if (ImGui.MenuItem("About"))
        {
            OpenAboutDialog = true;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Shows information about PewPew Mesh Studio.");

        ImGui.EndMenu();
    }
    private void DebugMenu()
    {
        if (!ImGui.BeginMenu("Debug"))
            return;

        if (ImGui.MenuItem("Error"))
        {
            OpenErrorDialog = true;
        }

        ImGui.EndMenu();
    }
}
