using ImGuiNET;
using System;

namespace PewPewMeshStudio.UI;

public class ProgramMenu
{
    public bool OpenFileDialog;
    public bool OpenErrorDialog;
    public bool OpenAboutDialog;

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
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Create a new project.");
        ImGui.MenuItem("Open");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Open a project file.");
        ImGui.MenuItem("Save", "Ctrl+S");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Saves the current project file.");
        ImGui.MenuItem("Save as..");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Saves the current project file in the desired location.");

        ImGui.Separator();

        ImGui.MenuItem("Import");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Load a PewPew mesh or an SVG.");
        ImGui.MenuItem("Export");
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

        ImGui.MenuItem("Preferences");
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
