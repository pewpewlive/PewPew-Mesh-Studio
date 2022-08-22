using System.Numerics;
using System.Runtime.Serialization;

namespace PewPewMeshStudio.PPMP;

/// <summary>
/// A class that holds the editor's state.
/// </summary>
[DataContract]
public class MeshProject
{
    [DataMember]
    public ushort PpmpRevision { get; set; }

    // TODO: encode mesh data directly into PPMP file
    [DataMember]
    public string[]? CurrentMeshes { get; set; }

    [DataMember]
    public int[]? CurrentMeshesIndexes { get; set; }

    [DataMember]
    public Vector3 CurrentPos { get; set; }

}
