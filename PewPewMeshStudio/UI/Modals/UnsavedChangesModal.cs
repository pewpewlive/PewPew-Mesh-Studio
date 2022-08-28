using ImGuiNET;
using PewPewMeshStudio.ExtraUtils;
using Serilog;
using System.Numerics;

namespace PewPewMeshStudio.UI.Modals;

public class UnsavedChangesModal
{
    public bool open;
    public static bool dontShowThisAgain = false;

    public void Initialize()
    {
        if (UIHandler.openModals == UIHandler.OpenModals.UnsavedChanges)
            open = true;

        ImGui.OpenPopup(I18n.c.GetString("Alert"));

        if (!ImGui.BeginPopupModal(I18n.c.GetString("Alert"), ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }

        ImGui.TextColored(ColorUtil.Vec4ByteToFloat(new Vector4(225, 50, 50, 255)), I18n.c.GetString("You have unsaved changes\nYou sure you want to quit?"));
        ImGui.Checkbox(I18n.c.GetString("Don't show this again"), ref dontShowThisAgain);

        ImGui.Separator();

        ImGui.PushStyleColor(ImGuiCol.Button, ColorUtil.Vec4ByteToFloat(new Vector4(200, 50, 50, 255)));
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ColorUtil.Vec4ByteToFloat(new Vector4(150, 50, 50, 255)));
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, ColorUtil.Vec4ByteToFloat(new Vector4(100, 50, 50, 255)));
        if (ImGui.Button(I18n.c.GetString("Quit")))
        {
            Log.CloseAndFlush();
            Environment.Exit(0);
        }

        ImGui.PopStyleColor(3);

        ImGui.SameLine();
        if(ImGui.Button(I18n.c.GetString("Save & Quit")))
        {
            Log.CloseAndFlush();
            Environment.Exit(0);
        }

        ImGui.SameLine();
        if (ImGui.Button(I18n.c.GetString("Cancel")))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }

        ImGui.EndPopup();
    }
}
