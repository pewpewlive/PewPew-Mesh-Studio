﻿using ImGuiNET;

namespace PewPewMeshStudio.UI;

public class ErrorPopup
{
    public bool open;

    public string errorMessage = "";

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup("Error encountered");

        if (!ImGui.BeginPopupModal("Error encountered", ref open))
        {
            open1 = false;
            return;
        }

        ImGui.Text("Error: *error string here*");

        if (ImGui.Button("Exit application"))
        {
            Environment.Exit(1); // send an error exit code
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Exits the application with a non-zero exit code.");

        if (ImGui.Button("Continue"))
        {
            open1 = false;
            return;
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Continue running the application.");
        ImGui.EndPopup();
    }
}
