namespace PewPewMeshStudio.LuaUtils;

public class ParserExceptions
{
    [Serializable]
    public class InvalidMeshIndex : Exception
    {
        public InvalidMeshIndex(string filename, int index)
            : base(string.Format("{0}:0: global 'meshes' does not contain a mesh at given index ({1})", filename, index)) { }
    }

    [Serializable]
    public class NoVertexTable : Exception
    {
        public NoVertexTable(string filename, int index)
            : base(string.Format("{0}:0: mesh at index ({1}) does not contain a valid 'vertexes' table", filename, index)) { }
    }

    [Serializable]
    public class NoSegmentTable : Exception
    {
        public NoSegmentTable(string filename, int index)
            : base(string.Format("{0}:0: mesh at index ({1}) does not contain a valid 'segments' table", filename, index)) { }
    }

    [Serializable]
    public class InvalidVertexIndex : Exception
    {
        public InvalidVertexIndex(string filename, int index, int size)
            : base(string.Format("{0}:0: a vertex index used in a segment in mesh at index ({1}) must be within the valid range [0,{2})", filename, index, size)) { }
    }

    [Serializable]
    public class InvalidSegment : Exception
    {
        public InvalidSegment(string filename, int index)
            : base(string.Format("{0}:0: the table 'segments' in mesh at index ({1}) can not contain a single vertex index segment", filename, index)) { }
    }
}