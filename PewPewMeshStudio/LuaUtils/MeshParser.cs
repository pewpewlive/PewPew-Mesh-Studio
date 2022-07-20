using NLua;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.LuaUtils
{
    public class MeshParser
    {
        public static Color4 LongToColor4(object Colore)
        {
            long Color = (long)Colore;
            return new Color4(
                (Color >> 24) & 255,
                (Color >> 16) & 255,
                (Color >> 8) & 255,
                Color & 255
            );
        }
        public static void ReturnMeshDict(string filepath, int index)
        {
            using Lua lua = new();
            lua.DoFile(filepath);
            lua.DoString(string.Format("vertexes = meshes[{0}].vertexes segments = meshes[{0}].segments colors = meshes[{0}].colors", index + 1)); // work smart, not hard
            Dictionary<object, object> VertexesDict = lua.GetTableDict(lua.GetTable("vertexes"));
            Dictionary<object, object> SegmentsDict = lua.GetTableDict(lua.GetTable("segments"));
            Dictionary<object, object> ColorsDict = lua.GetTableDict(lua.GetTable("colors"));
            lua.DoString("vertexes = nil segments = nil colors = nil");

            List<Color4> Colors = new();
            List<float[]> Vertexes = new();
            List<List<uint>> Segments = new();

            foreach (KeyValuePair<object, object> item in VertexesDict)
            {
                Dictionary<object, object> it = lua.GetTableDict((LuaTable)item.Value);

                object x;
                object y;
                object z;

                #pragma warning disable CS8600 // convert to nullable type
                it.TryGetValue(1, out x);
                it.TryGetValue(2, out y);
                it.TryGetValue(3, out z);
                #pragma warning restore CS8600 // convert to nullable type

                #pragma warning disable CS8605 // Unboxing a possibly null value.
                Vertexes.Add(new float[3] { (float)x, (float)y, (float)z });
                #pragma warning restore CS8605 // Unboxing a possibly null value.

                Console.WriteLine("Key = {0}, Value = {1}, ValueType = {2}", item.Key, item.Value, item.Value.GetType());
            }
            foreach (KeyValuePair<object, object> item in SegmentsDict)
            {
                foreach (KeyValuePair<object, object> it in lua.GetTableDict((LuaTable)item.Value))
                {
                    List<int> TempList = new();

                }
                Console.WriteLine("Key = {0}, Value = {1}, ValueType = {2}", item.Key, item.Value, item.Value.GetType());
            }
            foreach (KeyValuePair<object, object> item in ColorsDict)
            {
                Colors.Add(LongToColor4(item.Value));
            }
            Console.WriteLine(Colors);
        }
    }
}