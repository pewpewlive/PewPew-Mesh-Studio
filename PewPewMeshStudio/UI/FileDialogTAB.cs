using ImGuiNET;
using System.IO;
using System.Numerics;

namespace PewPewMeshStudio.UI;

public class FileDialogTab
{
    public bool open;

    public bool allowMultiSelect;

    string fileName = "";
    string path = "";

    string[] Directories = new string[0];
    string[] Files = new string[0];
    string[] Drives = new string[0];

    bool inDrivesList;
    bool refreshDirectory = true;

    // Path Working Directory
    string pwd = Directory.GetCurrentDirectory(); 

    public void Initialize(ref bool open1)
    {
        ImGui.OpenPopup("File Dialog");

        if (!ImGui.BeginPopupModal("File Dialog", ref open))
        {
            open1 = false;
            return;
        }

        if (ImGui.Button("Refresh"))
            refreshDirectory = true;
        ImGui.SameLine();
        if (ImGui.Button("Drives"))
            inDrivesList = true;
        ImGui.SameLine();
        ImGui.Text("Path:");
        ImGui.SameLine();

        ImGui.SetNextItemWidth(ImGui.GetWindowSize().X - 200f);
        ImGui.InputText("", ref pwd, 100);

        ImGui.BeginChild("fileList", new Vector2(0f, ImGui.GetWindowSize().Y - 100f), true, ImGuiWindowFlags.HorizontalScrollbar);

        ImGui.GetWindowSize();

        if (inDrivesList)
            UpdateDrivesList();
        else
            UpdateDirectoryList();
            
        ImGui.EndChild();

        ImGui.Button("Cancel");
        ImGui.SameLine();
        if (ImGui.Button("Save"))
        {
            //FileStream stream = new FileStream(pwd + @$"\{fileName}.ppmp" , FileMode.Create);

            // write the project file

            //stream.Close();
        }
        ImGui.SameLine();
        ImGui.Button("New Folder");

        ImGui.SameLine();
        ImGui.Text("Name:");
        ImGui.SameLine();

        ImGui.SetNextItemWidth(ImGui.GetWindowSize().X - 375f);
        ImGui.InputText(".ppmp", ref fileName, 100);

        ImGui.EndPopup();
    }

    private void UpdateDirectoryList()
    {

        if (refreshDirectory)
        {
            Directories = Directory.GetDirectories(pwd, "*", SearchOption.TopDirectoryOnly);
            Files = Directory.GetFiles(pwd, "*", SearchOption.TopDirectoryOnly);

            refreshDirectory = false;
        }

        if (ImGui.Selectable("..\\")) //prev folder
        {
            //Console.WriteLine(pwd);

            if (pwd == Directory.GetDirectoryRoot(pwd)) inDrivesList = true;

            pwd = pwd == Directory.GetDirectoryRoot(pwd) ? Directory.GetDirectoryRoot(pwd) : Directory.GetParent(pwd).FullName;//pwd.Remove((pwd.Length - Path.GetFileName(pwd).Length), Path.GetFileName(pwd).Length);

            refreshDirectory = true;

            //Console.WriteLine(pwd + "\n");
        }

        ImGui.Separator();

        foreach (string dir in Directories)
        {
            if (ImGui.Selectable(Path.GetFileName(dir) + "\\"))
            {
                pwd += @"\" + Path.GetFileName(dir);

                refreshDirectory = true;

                //Console.WriteLine(pwd);
            }
        }

        ImGui.Separator();

        foreach (string file in Files)
        {
            if (ImGui.Selectable(Path.GetFileName(file)))
            {
                fileName = Path.GetFileName(file);
            }
        }
    }

    private void UpdateDrivesList()
    {
        if (refreshDirectory)
            Drives = Directory.GetLogicalDrives();

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
}
