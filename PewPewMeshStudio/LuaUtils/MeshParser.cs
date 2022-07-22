using NLua;
using OpenTK.Mathematics;
//using PewPewMeshStudio.Renderer; 

namespace PewPewMeshStudio.LuaUtils;

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
        try
        {
            using Lua lua = new Lua();
            lua.DoFile(filepath);

            Dictionary<object, object> MeshesDict = lua.GetTableDict(lua.GetTable("meshes"));

            List<Color4> Colors = new List<Color4>();
            List<List<float>> Vertexes = new List<List<float>>();
            List<List<uint>> Segments = new List<List<uint>>();

            foreach (KeyValuePair<object, object> KVPair in MeshesDict)
            {
                if (Convert.ToInt32(KVPair.Key) != index)
                    continue;

                Dictionary<object, object> MeshDict = lua.GetTableDict((LuaTable)KVPair.Value);

                if (!MeshDict.ContainsKey("vertexes"))
                    throw new ParserExceptions.NoVertexTable(filepath, index);

                Dictionary<object, object> VertexesDict = lua.GetTableDict((LuaTable)MeshDict["vertexes"]);

                foreach (KeyValuePair<object, object> VertexItem in VertexesDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)VertexItem.Value);

                    List<float> Vertex = new List<float>();

                    foreach (KeyValuePair<object, object> Coord in Item)
                        Vertex.Add(Convert.ToSingle(Coord.Value));

                    Vertexes.Add(Vertex);
                }

                if (!MeshDict.ContainsKey("segments"))
                    throw new ParserExceptions.NoSegmentTable(filepath, index);

                Dictionary<object, object> SegmentsDict = lua.GetTableDict((LuaTable)MeshDict["segments"]);

                foreach (KeyValuePair<object, object> SegmentItem in SegmentsDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)SegmentItem.Value);

                    List<uint> Segment = new List<uint>();

                    foreach (KeyValuePair<object, object> VertexIndex in Item)
                    {
                        if (Convert.ToUInt32(VertexIndex.Value) < Vertexes.Count)
                            Segment.Add(Convert.ToUInt32(VertexIndex.Value));
                        else 
                            throw new ParserExceptions.InvalidVertexIndex(filepath, index, Vertexes.Count);
                    }

                    if (Segment.Count == 1) 
                        throw new ParserExceptions.InvalidSegment(filepath, index);

                    Segments.Add(Segment);
                }

                if (MeshDict.ContainsKey("colors"))
                {
                    Dictionary<object, object> ColorsDict = lua.GetTableDict((LuaTable)MeshDict["colors"]);

                    foreach (KeyValuePair<object, object> ColorItem in ColorsDict)
                    {
                        Colors.Add(LongToColor4(Convert.ToInt64(ColorItem.Value)));
                    }

                    if (Colors.Count > Vertexes.Count)
                        Colors.RemoveRange(Vertexes.Count, Colors.Count - Vertexes.Count);
                } 

                for (int i = Colors.Count; i < Vertexes.Count; i++)
                    Colors.Add(LongToColor4(Convert.ToInt64(0xffffffff)));

                break;
            }

            if (Vertexes.Count == 0 && Segments.Count == 0 && Colors.Count == 0)
                throw new ParserExceptions.InvalidMeshIndex(filepath, index);

            //return new Renderable(new MeshObject());
        }
        catch (Exception Ex)
        {
            Console.WriteLine(string.Format("[Error]: Mesh Parser -> {0}", Ex.Message));
            Console.WriteLine("[Error]: Mesh Parser -> Failed to parse mesh file! Returning empty mesh object.");
            //return new Renderable(new MeshObject());
        }
    }
}