using PewPewMeshStudio.Core;
using PewPewMeshStudio.LuaUtils;
using PewPewMeshStudio.ExtraUtils;

namespace PewPewMeshStudio
{
    class Program
    {
        static void Main()
        {
            //ConsoleExtension.Show();
            MeshParser.ParseMeshFile("mesh.lua", 1);

            Window MainWindow = new();
            MainWindow.Run();
        }
    }
}