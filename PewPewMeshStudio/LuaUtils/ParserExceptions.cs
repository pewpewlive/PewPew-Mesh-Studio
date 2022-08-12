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
    public class InvalidVertexCoordCount : Exception
    {
        public InvalidVertexCoordCount(string filename, int index)
            : base(string.Format("{0}:0: the table 'vertexes' in mesh at index ({1}) countains a vertex with an invalid amount of position values", filename, index)) { }
    }

    [Serializable]
    public class InvalidColorCount : Exception
    {
        public InvalidColorCount(string filename, int index)
            : base(string.Format("{0}:0: the table 'colors' in mesh at index ({1}) must be the same size as 'vertexes'", filename, index)) { }
    }

    [Serializable]
    public class InvalidVertexIndexInSegment : Exception
    {
        public InvalidVertexIndexInSegment(string filename, int index, int index2, int size)
            : base(string.Format("{0}:0: a vertex index ({1}) used in a segment in mesh at index ({2}) must be within the valid range [0,{3})", filename, index, index2, size)) { }
    }

    [Serializable]
    public class InvalidSegmentIndexCount : Exception
    {
        public InvalidSegmentIndexCount(string filename, int index)
            : base(string.Format("{0}:0: the table 'segments' in mesh at index ({1}) can not contain a single vertex index segment", filename, index)) { }
    }
}