using ImGuiNET;
using System;

namespace PewPewMeshStudio.UI;

public class ProgramMenu
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
        if (!ImGui.BeginMenu("File"))
            return;

        if (ImGui.MenuItem("New"))
        {
            OpenFileDialog = true;
            fileDialogType = 0;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Create a new project.");

        if (ImGui.MenuItem("Open"))
        {
            OpenFileDialog = true;
            fileDialogType = 1;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Open a project file.");

        ImGui.MenuItem("Save", "Ctrl+S"); // save
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Saves the current project file.");

        if (ImGui.MenuItem("Save as.."))
        {
            OpenFileDialog = true;
            fileDialogType = 2;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Saves the current project file in the desired location.");

        ImGui.Separator();

        if (ImGui.MenuItem("Import"))
        {
            OpenFileDialog = true;
            fileDialogType = 3;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Load a PewPew mesh or an SVG.");

        if (ImGui.MenuItem("Export"))
        {
            OpenFileDialog = true;
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
