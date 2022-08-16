using NLua;
using System.Numerics;
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.ExtraUtils;
using Serilog;

namespace PewPewMeshStudio.LuaUtils;

public class MeshParser
{
    public static Vector4 LongToVector4(long Color)
    {
        return new Vector4(
            (Color >> 24) & 255,
            (Color >> 16) & 255,
            (Color >> 8) & 255,
             Color & 255
        );
    }

    public static Renderable ParseMeshFile(string FilePath, int MeshIndex)
    {
        try
        {
            using Lua lua = new Lua();
            lua.DoString("os = nil io = nil debug = nil coroutine = nil utf8 = nil"); // disable potentially dangerous or not supported by PPL libraries from loading
            lua.DoFile(FilePath);

            Dictionary<object, object> MeshesDict = lua.GetTableDict(lua.GetTable("meshes"));

            List<MeshVertex> VertexData = new List<MeshVertex>();
            List<uint[]> Segments = new List<uint[]>();

            foreach (KeyValuePair<object, object> KVPair in MeshesDict)
            {
                if (Convert.ToInt32(KVPair.Key) != MeshIndex)
                    continue;

                Dictionary<object, object> MeshDict = lua.GetTableDict((LuaTable)KVPair.Value);

                if (!MeshDict.ContainsKey("vertexes"))
                    throw new ParserExceptions.NoVertexTable(FilePath, MeshIndex);

                Dictionary<object, object> VertexesDict = lua.GetTableDict((LuaTable)MeshDict["vertexes"]);
                Dictionary<object, object> ColorsDict = MeshDict.ContainsKey("colors") ? lua.GetTableDict((LuaTable)MeshDict["colors"]) : new Dictionary<object, object>();

                if (ColorsDict.Count != 0 && ColorsDict.Count != VertexesDict.Count)
                    throw new ParserExceptions.InvalidColorCount(FilePath, MeshIndex);

                foreach (KeyValuePair<object, object> VertexItem in VertexesDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)VertexItem.Value);

                    List<float> Position = new List<float>();

                    foreach (KeyValuePair<object, object> Coord in Item)
                        Position.Add(Convert.ToSingle(Coord.Value));

                    if (Position.Count == 2)
                        Position.Add(Convert.ToSingle(0));

                    if (Position.Count != 3)
                        throw new ParserExceptions.InvalidVertexCoordCount(FilePath, MeshIndex);

                    Vector4 Color = ColorsDict.Count == 0 ? ColorUtil.Vec4ByteToFloat(LongToVector4(0xffffffff)) : 
                                                            ColorUtil.Vec4ByteToFloat(LongToVector4(Convert.ToInt64(ColorsDict[VertexItem.Key])));

                    VertexData.Add(new MeshVertex(new OpenTK.Mathematics.Vector3(Position[0], Position[1], Position[2]),
                                                  new OpenTK.Mathematics.Vector4(Color.X, Color.Y, Color.Z, Color.W)));
                }

                if (!MeshDict.ContainsKey("segments"))
                    throw new ParserExceptions.NoSegmentTable(FilePath, MeshIndex);

                Dictionary<object, object> SegmentsDict = lua.GetTableDict((LuaTable)MeshDict["segments"]);

                foreach (KeyValuePair<object, object> SegmentItem in SegmentsDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)SegmentItem.Value);

                    List<uint> Segment = new List<uint>();

                    foreach (KeyValuePair<object, object> VertexIndex in Item)
                    {
                        int Index = Convert.ToInt32(VertexIndex.Value);
                        if (Index < VertexesDict.Count && Index >= 0)
                            Segment.Add(Convert.ToUInt32(Index));
                        else 
                            throw new ParserExceptions.InvalidVertexIndexInSegment(FilePath, Index, MeshIndex, VertexesDict.Count);
                    }

                    if (Segment.Count < 2) 
                        throw new ParserExceptions.InvalidSegmentIndexCount(FilePath, MeshIndex);

                    Segments.Add(Segment.ToArray());
                }

                break;
            }

            if (VertexData.Count == 0 && Segments.Count == 0)
                throw new ParserExceptions.InvalidMeshIndex(FilePath, MeshIndex);

            lua.Dispose();
            lua.Close();

            return new Renderable(VertexData.ToArray(), Segments.ToArray());
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(MeshParser) Failed to parse mesh file! Returning empty mesh object.");
            return new Renderable(Array.Empty<MeshVertex>(), Array.Empty<uint[]>());
        }
    }
    public static Renderable ParseMeshString(string LuaCode, int MeshIndex)
    {
        try
        {
            using Lua lua = new Lua();
            lua.DoString("os = nil io = nil debug = nil coroutine = nil utf8 = nil"); // disable potentially dangerous or not supported by PPL libraries from loading
            lua.DoString(LuaCode);

            Dictionary<object, object> MeshesDict = lua.GetTableDict(lua.GetTable("meshes"));

            List<MeshVertex> VertexData = new List<MeshVertex>();
            List<uint[]> Segments = new List<uint[]>();

            foreach (KeyValuePair<object, object> KVPair in MeshesDict)
            {
                if (Convert.ToInt32(KVPair.Key) != MeshIndex)
                    continue;

                Dictionary<object, object> MeshDict = lua.GetTableDict((LuaTable)KVPair.Value);

                if (!MeshDict.ContainsKey("vertexes"))
                    throw new ParserExceptions.NoVertexTable("internal", MeshIndex);

                Dictionary<object, object> VertexesDict = lua.GetTableDict((LuaTable)MeshDict["vertexes"]);
                Dictionary<object, object> ColorsDict = MeshDict.ContainsKey("colors") ? lua.GetTableDict((LuaTable)MeshDict["colors"]) : new Dictionary<object, object>();

                if (ColorsDict.Count != 0 && ColorsDict.Count != VertexesDict.Count)
                    throw new ParserExceptions.InvalidColorCount("internal", MeshIndex);

                foreach (KeyValuePair<object, object> VertexItem in VertexesDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)VertexItem.Value);

                    List<float> Position = new List<float>();

                    foreach (KeyValuePair<object, object> Coord in Item)
                        Position.Add(Convert.ToSingle(Coord.Value));

                    if (Position.Count == 2)
                        Position.Add(Convert.ToSingle(0));

                    if (Position.Count != 3)
                        throw new ParserExceptions.InvalidVertexCoordCount("internal", MeshIndex);

                    Vector4 Color = ColorsDict.Count == 0 ? ColorUtil.Vec4ByteToFloat(LongToVector4(0xffffffff)) :
                                                            ColorUtil.Vec4ByteToFloat(LongToVector4(Convert.ToInt64(ColorsDict[VertexItem.Key])));

                    VertexData.Add(new MeshVertex(new OpenTK.Mathematics.Vector3(Position[0], Position[1], Position[2]),
                                                  new OpenTK.Mathematics.Vector4(Color.X, Color.Y, Color.Z, Color.W)));
                }

                if (!MeshDict.ContainsKey("segments"))
                    throw new ParserExceptions.NoSegmentTable("internal", MeshIndex);

                Dictionary<object, object> SegmentsDict = lua.GetTableDict((LuaTable)MeshDict["segments"]);

                foreach (KeyValuePair<object, object> SegmentItem in SegmentsDict)
                {
                    Dictionary<object, object> Item = lua.GetTableDict((LuaTable)SegmentItem.Value);

                    List<uint> Segment = new List<uint>();

                    foreach (KeyValuePair<object, object> VertexIndex in Item)
                    {
                        int Index = Convert.ToInt32(VertexIndex.Value);
                        if (Index < VertexesDict.Count && Index >= 0)
                            Segment.Add(Convert.ToUInt32(Index));
                        else
                            throw new ParserExceptions.InvalidVertexIndexInSegment("internal", Index, MeshIndex, VertexesDict.Count);
                    }

                    if (Segment.Count < 2)
                        throw new ParserExceptions.InvalidSegmentIndexCount("internal", MeshIndex);

                    Segments.Add(Segment.ToArray());
                }

                break;
            }

            if (VertexData.Count == 0 && Segments.Count == 0)
                throw new ParserExceptions.InvalidMeshIndex("internal", MeshIndex);

            lua.Dispose();
            lua.Close();

            return new Renderable(VertexData.ToArray(), Segments.ToArray());
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(MeshParser) Failed to parse mesh file! Returning empty mesh object.");
            return new Renderable(Array.Empty<MeshVertex>(), Array.Empty<uint[]>());
        }
    }
}