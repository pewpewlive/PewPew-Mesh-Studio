using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using System.Numerics;

namespace PewPewMeshStudio.UI.Modals;

public class UnsavedChangesModal
{
    public bool open;
    public bool dontShowThisAgain = false;

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup(I18n.c.GetString("Alert"));
        if (!ImGui.BeginPopupModal(I18n.c.GetString("Alert"), ref open))
        {
            open1 = false;
            return;
        }

        ImGui.TextColored(ColorUtil.Vec4ByteToFloat(new Vector4(225, 50, 50, 255)), I18n.c.GetString("You have unsaved changes\nYou sure you want to quit?"));
        ImGui.Checkbox(I18n.c.GetString("Don't show this again"), ref dontShowThisAgain);

        ImGui.Separator();

        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(200, 50, 50, 255)));
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ColorUtil.Vec4ByteToFloat(new Vector4(150, 50, 50, 255)));
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, ColorUtil.Vec4ByteToFloat(new Vector4(100, 50, 50, 255)));
        ImGui.Button(I18n.c.GetString("Quit"));
        ImGui.PopStyleColor(3);

        ImGui.SameLine();
        ImGui.Button(I18n.c.GetString("Save & Quit"));
        ImGui.SameLine();
        ImGui.Button(I18n.c.GetString("Cancel"));

        ImGui.EndPopup();
    }
}
