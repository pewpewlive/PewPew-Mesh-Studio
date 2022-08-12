using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace PewPewMeshStudio.Renderer;

public struct MeshVertex
{
    public Vector3 Position;
    public Vector4 Color;

    public MeshVertex(Vector3 NewPosition, Vector4 NewColor)
    {
        Position = NewPosition;
        Color = NewColor;
    }
}