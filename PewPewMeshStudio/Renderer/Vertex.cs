using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace PewPewMeshStudio.Renderer;

public struct MeshVertex
{
    public Vector3 Position;
    public Vector4 Color;

    public MeshVertex(Vector3 Position, Vector4 Color)
    {
        this.Position = Position;
        this.Color = Color;
    }
}

public struct RenderableVertex
{
    public Vector3 Previous;
    public Vector3 Current;
    public Vector3 Next;
    public Vector4 Color;
    public float MiterLength;

    public RenderableVertex(Vector3 Previous, Vector3 Current, Vector3 Next, Vector4 Color, float MiterLength)
    {
        this.Previous = Previous;
        this.Current = Current;
        this.Next = Next;
        this.Color = Color;
        this.MiterLength = MiterLength;
    }
}