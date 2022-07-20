using PewPewMeshStudio.Core;
using PewPewMeshStudio.LuaUtils;

namespace PewPewMeshStudio
{
    class Program
    {
        static void Main()
        {
            MeshParser.ReturnMeshDict("mesh.lua", 1);

            Window MainWindow = new();
            MainWindow.Run();
        }
    }
}