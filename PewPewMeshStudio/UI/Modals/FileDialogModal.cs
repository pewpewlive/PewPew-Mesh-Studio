using ImGuiNET;
using System.IO;
using System.Numerics;
using Serilog;

namespace PewPewMeshStudio.UI.Modals;

public class FileDialogModal
{
    public bool open;

    public bool allowMultiSelect;

    string fileName = "";
    string path = "";

    string[] Directories = new string[0];
    string[] Files = new string[0];
    string[] Drives = new string[0];


    // Path Working Directory
    string pwd = Directory.GetCurrentDirectory();
    string inputDir = Directory.GetCurrentDirectory();

    bool inDrivesList;
    bool refreshDirectory = true;

    public string[] supportedExtentions = { ".ppmp", ".lua" };

    public enum FileDialogType
    {
        NewProject = 0,
        OpenProject = 1,

        SaveProjectAs = 2,

        ImportMesh = 3,
        ExportMesh = 4
    }

    FileDialogType FDType;

    public void Initialize(int fdt)
    {
        FDType = (FileDialogType)fdt;

        if (UIHandler.openModals == UIHandler.OpenModals.FileDialog)
            open = true;

        ImGui.OpenPopup("File Dialog");

        if (!ImGui.BeginPopupModal("File Dialog", ref open))
        {
            UIHandler.openModals = UIHandler.OpenModals.None;
            return;
        }

        if (ImGui.Button("Refresh"))
            refreshDirectory = true;

        ImGui.SameLine();
        if (ImGui.Button("Drives"))
        {
            inDrivesList = true;
            refreshDirectory = true;
        }

        ImGui.SameLine();
        ImGui.Text("Path:");

        UpdateDirectoryBar();

        ImGui.BeginChild("fileList", new Vector2(0f, ImGui.GetWindowSize().Y - 103f), true, ImGuiWindowFlags.HorizontalScrollbar);

        if (inDrivesList)
            UpdateDrivesList();
        else
            UpdateDirectoryList();

        ImGui.EndChild();

        ImGui.Button("Cancel");

        UpdateContextButtons();

        ImGui.SameLine();
        ImGui.Button("New Folder");

        ImGui.SameLine();

        ImGui.SetNextItemWidth(ImGui.GetWindowWidth() - 300f);

        if (FDType == (FileDialogType.NewProject & FileDialogType.SaveProjectAs & FileDialogType.ExportMesh))
            ImGui.InputTextWithHint(".ppmp", "File name", ref fileName, 100);

        else if (FDType == (FileDialogType.OpenProject & FileDialogType.ImportMesh))
            ImGui.InputTextWithHint(".ppmp", "File name", ref fileName, 100, ImGuiInputTextFlags.ReadOnly);

        ImGui.EndPopup();
    }

    private void UpdateDirectoryBar()
    {
        ImGui.SameLine();
        ImGui.SetNextItemWidth(ImGui.GetWindowWidth() - 185f);

        bool dirChanged = ImGui.InputTextWithHint("", "Enter path here", ref inputDir, 200, ImGuiInputTextFlags.EnterReturnsTrue);

        if (refreshDirectory)
            inputDir = pwd;

        if (dirChanged)
            if (Directory.Exists(inputDir))
            {
                if (!inputDir.Contains("\\"))
                    inputDir += "\\";

                pwd = inputDir;

                refreshDirectory = true;
            }
            else
                inputDir = pwd;
    }

    private void UpdateDirectoryList()
    {
        if (refreshDirectory)
        {
            try
            {
                Directories = Directory.GetDirectories(pwd, "*", SearchOption.TopDirectoryOnly);
                Files = Directory.GetFiles(pwd, "*", SearchOption.TopDirectoryOnly);
                refreshDirectory = false;

                ImGui.SetScrollY(0f);
            }
            catch
            {
                Log.Warning("(FileDialogModal) Access to this directory is denied.");

                refreshDirectory = true;
                inDrivesList = true;
            }
        }

        if (ImGui.Selectable("..\\")) //prev folder
        {
            //Console.WriteLine(pwd);

            if (pwd == Directory.GetDirectoryRoot(pwd)) inDrivesList = true;
            pwd = pwd == Directory.GetDirectoryRoot(pwd) ? Directory.GetDirectoryRoot(pwd) : Directory.GetParent(pwd).FullName;//pwd.Remove((pwd.Length - Path.GetFileName(pwd).Length), Path.GetFileName(pwd).Length);

            refreshDirectory = true;

            //Console.WriteLine(pwd + "\n");
        }

        if (Directories.Length != 0)
            ImGui.Separator();

        foreach (string dir in Directories)
        {
            if (ImGui.Selectable(Path.GetFileName(dir) + "\\"))
            {
                pwd += "\\" + Path.GetFileName(dir);

                refreshDirectory = true;

                //Console.WriteLine(pwd);
            }
        }

        if (Files.Length != 0)
            ImGui.Separator();

        foreach (string file in Files)
        {
            //Path.GetExtension(file)
            if (ImGui.Selectable(Path.GetFileName(file)))
            {
                fileName = Path.GetFileNameWithoutExtension(file);
            }
        }
    }

    private void UpdateDrivesList()
    {
        if (refreshDirectory)
        {
            Drives = Directory.GetLogicalDrives();
            inputDir = "";

            refreshDirectory = false;
        }

        foreach (string drive in Drives)
        {
            if (ImGui.Selectable(drive))
            {
                pwd = drive;

                inDrivesList = false;
                refreshDirectory = true;
            }
        }
    }

    private void UpdateContextButtons()
    {
        ImGui.SameLine();

        switch (FDType)
        {
            case FileDialogType.NewProject: // NewProject
                if (fileName == "") ImGui.BeginDisabled(true);

                if (ImGui.Button("Create"))
                {
                }

                ImGui.EndDisabled();
                return;
            case FileDialogType.OpenProject: // OpenProject
                if (ImGui.Button("Open"))
                {
                }
                return;
            case FileDialogType.SaveProjectAs: // SaveProjectAs
                if (fileName == "") ImGui.BeginDisabled(true);

                if (ImGui.Button("Save"))
                {
                }

                ImGui.EndDisabled();
                return;
            case FileDialogType.ImportMesh: // ImportMesh
                if (ImGui.Button("Import"))
                {
                }
                return;
            case FileDialogType.ExportMesh: // ExportMesh
                if (fileName == "") ImGui.BeginDisabled(true);

                if (ImGui.Button("Export"))
                {
                }

                ImGui.EndDisabled();
                return;

            default:
                Log.Error("(FileDialogModal) Invalid context button index.");
                return;
        }
    }
}
