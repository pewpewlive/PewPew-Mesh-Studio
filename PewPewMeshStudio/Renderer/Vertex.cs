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

public struct RenderableVertex
{
    public Vector3 Previous;
    public Vector3 Current;
    public Vector3 Next;
    public Vector4 Color;

    public float Sign;

    public RenderableVertex(Vector3 NewPrevious, Vector3 NewCurrent, Vector3 NewNext, Vector4 NewColor, float NewSign)
    {
        Previous = NewPrevious;
        Current = NewCurrent;
        Next = NewNext;
        Color = NewColor;
        Sign = NewSign;
    }
}