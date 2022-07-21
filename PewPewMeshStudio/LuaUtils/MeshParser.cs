using NLua;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.LuaUtils
{
    public class MeshParser
    {
        public static Color4 LongToColor4(long color)
        {
            return new Color4(
                (color >> 24) & 255,
                (color >> 16) & 255,
                (color >> 8) & 255,
                color & 255
            );
        }

        public static void ParseMeshFile(string filepath, int index)
        {
            using Lua lua = new();
            lua.DoFile(filepath);
            Dictionary<object, object> MeshesDict = lua.GetTableDict(lua.GetTable("meshes"));

            List<Color4> Colors = new();
            List<List<float>> Vertexes = new();
            List<List<uint>> Segments = new();

            foreach (KeyValuePair<object, object> KVPair in MeshesDict)
            {
                if (Convert.ToInt32(KVPair.Key) != index)
                    continue;

                Dictionary<object, object> MeshDict = lua.GetTableDict((LuaTable)KVPair.Value);

                Dictionary<object, object> VertexesDict = lua.GetTableDict((LuaTable)MeshDict["vertexes"]);
                Dictionary<object, object> SegmentsDict = lua.GetTableDict((LuaTable)MeshDict["segments"]);
                Dictionary<object, object> ColorsDict = lua.GetTableDict((LuaTable)MeshDict["colors"]);

                foreach (KeyValuePair<object, object> VertexItem in VertexesDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)VertexItem.Value);

                    List<float> Vertex = new();

                    foreach (KeyValuePair<object, object> Coord in Item)
                        Vertex.Add(Convert.ToSingle(Coord.Value)); 

                    Vertexes.Add(Vertex);
                }

                foreach (KeyValuePair<object, object> SegmentItem in SegmentsDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)SegmentItem.Value);

                    List<uint> Segment = new();

                    foreach (KeyValuePair<object, object> SegmentIndex in Item)
                        Segment.Add(Convert.ToUInt32(SegmentIndex.Value));

                    Segments.Add(Segment);
                }

                foreach (KeyValuePair<object, object> ColorItem in ColorsDict)
                {
                    Colors.Add(LongToColor4(Convert.ToInt64(ColorItem.Value)));
                }

                break;
            }
            //Currently is void, will be later implemented to be used with the Renderable Class
        }
    }
}