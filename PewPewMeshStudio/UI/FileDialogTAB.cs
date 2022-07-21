using ImGuiNET;
using System.IO;

namespace PewPewMeshStudio.UI;

public class FileDialogTAB
{
    public bool open;

    public bool allowMultiSelect;

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup("file_dialog");

        if (!ImGui.BeginPopupModal("file_dialog", ref open))
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
        ImGui.Selectable("file.txt");

        ImGui.EndPopup();
    }
}
