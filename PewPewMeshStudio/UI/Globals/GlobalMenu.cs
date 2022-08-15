using ImGuiNET;
using System;

namespace PewPewMeshStudio.UI.Globals;

public class GlobalMenu
{
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
        if (!ImGui.BeginMenu("File"))
            return;

        if (ImGui.MenuItem("New"))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 0;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Create a new project.");

        if (ImGui.MenuItem("Open"))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 1;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Open a project file.");

        ImGui.MenuItem("Save", "Ctrl+S"); // save
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Saves the current project file.");

        if (ImGui.MenuItem("Save as.."))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 2;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Saves the current project file in the desired location.");

        ImGui.Separator();

        if (ImGui.MenuItem("Import"))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 3;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Load a PewPew mesh or an SVG.");

        if (ImGui.MenuItem("Export"))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 4;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Export the current project as a PewPew mesh.");

        ImGui.Separator();

        if (ImGui.MenuItem("Quit", "Alt+F4"))
        {
            Environment.Exit(0);
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Quit PewPew Mesh Studio.");

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
            UIHandler.openModals = UIHandler.OpenModals.Preferences;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Edit user preferences.");
        ImGui.Separator();

        if (ImGui.MenuItem("About"))
        {
            UIHandler.openModals = UIHandler.OpenModals.About;
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
            UIHandler.openModals = UIHandler.OpenModals.Error;
        }

        ImGui.EndMenu();
    }
}
