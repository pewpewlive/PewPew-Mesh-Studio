using ImGuiNET;
using OpenTK.Compute.OpenCL;
using PewPewMeshStudio.ExtraUtils;
using Serilog;

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
        if (!ImGui.BeginMenu(I18n.c.GetString("File")))
            return;

        if (ImGui.MenuItem(I18n.c.GetString("New")))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 0;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Create a new project."));

        if (ImGui.MenuItem(I18n.c.GetString("Open")))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 1;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Open a project file."));

        ImGui.MenuItem(I18n.c.GetString("Save"), "Ctrl+S"); // save
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Saves the current project file."));

        if (ImGui.MenuItem(I18n.c.GetString("Save as...")))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 2;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Saves the current project file in the desired location."));

        ImGui.Separator();

        if (ImGui.MenuItem(I18n.c.GetString("Import")))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 3;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Load a PewPew mesh or an SVG."));

        if (ImGui.MenuItem(I18n.c.GetString("Export")))
        {
            UIHandler.openModals = UIHandler.OpenModals.FileDialog;
            fileDialogType = 4;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Export the current project as a PewPew mesh."));

        ImGui.Separator();

        if (ImGui.MenuItem(I18n.c.GetString("Quit"), "Alt+F4"))
        {
            if (!Modals.UnsavedChangesModal.dontShowThisAgain)
                UIHandler.openModals = UIHandler.OpenModals.UnsavedChanges;
            else
            {
                Log.CloseAndFlush();
                Environment.Exit(0);
            }
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Quit PewPew Mesh Studio."));

        ImGui.EndMenu();
    }

    private void EditMenu()
    {
        if (!ImGui.BeginMenu(I18n.c.GetString("Edit")))
            return;

        ImGui.MenuItem(I18n.c.GetString("Undo"), "Ctrl+Z");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Undo previous action."));
        ImGui.MenuItem(I18n.c.GetString("Redo"), "Ctrl+Y");
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Redo previous action."));

        ImGui.Separator();

        if (ImGui.MenuItem(I18n.c.GetString("Preferences")))
        {
            UIHandler.openModals = UIHandler.OpenModals.Preferences;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Edit user preferences."));
        ImGui.Separator();

        if (ImGui.MenuItem(I18n.c.GetString("About")))
        {
            UIHandler.openModals = UIHandler.OpenModals.About;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(I18n.c.GetString("Shows information about PewPew Mesh Studio."));

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
        if (ImGui.MenuItem("Unsaved changes"))
        {
            UIHandler.openModals = UIHandler.OpenModals.UnsavedChanges;
        }
        if (ImGui.MenuItem("Splash Screen"))
        {
            UIHandler.openModals = UIHandler.OpenModals.SplashScreen;
        }
        ImGui.EndMenu();
    }
}
