using NLua;

namespace PewPewMeshStudio.LuaUtils
{
    public class MeshParser
    {
        public static object ReturnMesh(string filepath)
        {
            using Lua lua = new();
            lua.DoFile(filepath);
            object meshes = lua["meshes"];
            return meshes;
        }
    }
}