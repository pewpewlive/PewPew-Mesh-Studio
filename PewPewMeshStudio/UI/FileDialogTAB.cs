using ImGuiNET;
using System.IO;

namespace PewPewMeshStudio.UI;

public class FileDialogTab
{
    public bool open;

    public bool allowMultiSelect;

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup("File Dialog");

        if (!ImGui.BeginPopupModal("File Dialog", ref open))
        {
            open1 = false;
            return;
        }

        ImGui.Button("Refresh");
        ImGui.SameLine();
        ImGui.Button("Drives");

        ImGui.Separator();

        //ImGui.

        //ImGui.NewFrame();
        ImGui.Selectable("..\\");
        ImGui.Separator();
        ImGui.Selectable("folder1\\");
        ImGui.Selectable("folder2\\");
        ImGui.Selectable("folder3\\");
        ImGui.Separator();
        ImGui.Selectable("file.txt");
        ImGui.Selectable("mesh.lua");

        ImGui.EndPopup();
    }
}
