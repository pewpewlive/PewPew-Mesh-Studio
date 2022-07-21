using ImGuiNET;

namespace PewPewMeshStudio.UI;

public class ProgramMenu
{
    public bool openFileDialog;

    public void Initialize()
    {
        ImGui.BeginMainMenuBar();

        FileMENU();
        EditMENU();

        ImGui.EndMainMenuBar();
    }

    private void FileMENU()
    {
        if (!ImGui.BeginMenu("File"))
            return;

        if (ImGui.MenuItem("New"))
        {
            openFileDialog = true;
        }
        ImGui.MenuItem("Open");

        ImGui.MenuItem("Save", "Ctrl+S");
        ImGui.MenuItem("Save as..");

        ImGui.Separator();

        ImGui.MenuItem("Import");
        ImGui.MenuItem("Export");

        ImGui.Separator();

        ImGui.MenuItem("Quit", "Alt+F4");

        ImGui.EndMenu();
    }

    private void EditMENU()
    {
        if (!ImGui.BeginMenu("Edit"))
            return;

        ImGui.MenuItem("Undo", "Ctrl+Z");
        ImGui.MenuItem("Redo", "Ctrl+Y");

        ImGui.Separator();

        ImGui.MenuItem("Copy", "Ctrl+C");
        ImGui.MenuItem("Paste", "Ctrl+V");

        ImGui.Separator();

        ImGui.MenuItem("Preferences");

        ImGui.EndMenu();
    }
}
