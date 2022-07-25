using ImGuiNET;
using System.Numerics;

namespace PewPewMeshStudio.UI;

public class UnsavedChangesPopup
{
    public bool open = true;
    public bool dontShowThisAgain = false;

    public void Initialize()
    {
        ImGui.OpenPopup("Alert");
        if (!ImGui.BeginPopupModal("Alert", ref open))
            return;

        ImGui.TextColored(ColorUtil.Vec4ByteToFloat(new Vector4(225, 50, 50, 255)), "You have unsaved changes\nYou sure you want to quit?");
        ImGui.Checkbox("Don't show this again", ref dontShowThisAgain);

        ImGui.Separator();

        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(200, 50, 50, 255)));
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ColorUtil.Vec4ByteToFloat(new Vector4(150, 50, 50, 255)));
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, ColorUtil.Vec4ByteToFloat(new Vector4(100, 50, 50, 255)));
        ImGui.Button("Quit");
        ImGui.PopStyleColor(3);

        ImGui.SameLine();
        ImGui.Button("Save & Quit");
        ImGui.SameLine();
        ImGui.Button("Cancel");

        ImGui.EndPopup();
    }
}
