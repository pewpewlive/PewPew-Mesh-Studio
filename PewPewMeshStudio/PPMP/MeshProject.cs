using System.Numerics;
using System.Runtime.Serialization;

namespace PewPewMeshStudio.PPMP;

/// <summary>
/// A class that holds the editor's state.
/// </summary>
[DataContract]
public class MeshProject
{
    [DataMember(IsRequired = true, Order = 0)]
    public ushort PpmpRevision { get; set; }

    // TODO: encode mesh data directly into PPMP file
    [DataMember(Order = 1)]
    public string[]? CurrentMeshes { get; set; }

    [DataMember(Order = 1)]
    public int[]? CurrentMeshesIndexes { get; set; }

    [DataMember(Order = 1)]
    public bool[]? CurrentHiddenMeshes { get; set; }

    [DataMember(Order = 1)]
    public Vector3[]? CurrentMeshesPositions { get; set; }

    [DataMember(Order = 1)]
    public bool ShowColors { get; set; }

    [DataMember(Order = 1)]
    public bool IsIsometricView { get; set; }

    [DataMember(Order = 1)]
    public Vector3? CameraPos { get; set; }

    [DataMember(Order = 1)]
    public Vector2? CameraAngles { get; set; }
}
