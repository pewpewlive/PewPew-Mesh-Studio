using PewPewMeshStudio.Core;
using PewPewMeshStudio.LuaUtils;

namespace PewPewMeshStudio
{
    class Program
    {
        static void Main()
        {
            //object mesh = MeshParser.ReturnMesh("mesh.lua");
            //Console.WriteLine(mesh);

            Window MainWindow = new();
            MainWindow.Run();
        }
    }
}