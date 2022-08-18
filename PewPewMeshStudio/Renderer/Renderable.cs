using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace PewPewMeshStudio.Renderer;

public class Renderable
{
    private int VertexBuffer;
    private int VertexArray;
    private Tuple<int, int> ElementBuffer = new Tuple<int, int>(0, 0);

    private Shader RenderableShader = new Shader();

    private RenderableVertex[] VertexData;
    private uint[] SegmentData;

    public Renderable(MeshVertex[] LineVertexData, uint[][] Segments)
    {
        List<RenderableVertex> RenderableVertexData = new List<RenderableVertex>();
        List<uint> RenderableSegmentData = new List<uint>();

        foreach (uint[] Segment in Segments)
        {
            for (int i = 0; i < Segment.Length; i++)
            {
                Vector3 Previous, Current, Next;
                Vector4 Color = LineVertexData[Segment[i]].Color;

                if (i == 0)
                {
                    Current = LineVertexData[Segment[i]].Position;
                    Next = LineVertexData[Segment[i + 1]].Position;

                    if (Segment[0] == Segment[^1])
                        Previous = LineVertexData[Segment[^2]].Position;
                    else
                        Previous = Current - (Next - Current);
                } 
                else
                {
                    if (i >= 1 && i < Segment.Length - 1)
                    {
                        Previous = LineVertexData[Segment[i - 1]].Position;
                        Current = LineVertexData[Segment[i]].Position;
                        Next = LineVertexData[Segment[i + 1]].Position;
                    }
                    else
                    {
                        Previous = LineVertexData[Segment[i - 1]].Position;
                        Current = LineVertexData[Segment[i]].Position;

                        if (Segment[0] == Segment[^1])
                            Next = LineVertexData[Segment[1]].Position;
                        else
                            Next = Current + (Current - Previous);
                    }

                    RenderableSegmentData.AddRange(new uint[3] { Convert.ToUInt32(RenderableVertexData.Count + 1),
                                                                 Convert.ToUInt32(RenderableVertexData.Count),
                                                                 Convert.ToUInt32(RenderableVertexData.Count - 1) });

                    RenderableSegmentData.AddRange(new uint[3] { Convert.ToUInt32(RenderableVertexData.Count),
                                                                 Convert.ToUInt32(RenderableVertexData.Count - 1),
                                                                 Convert.ToUInt32(RenderableVertexData.Count - 2) });
                }

                Vector3 AB = (Current - Previous).Normalized();
                Vector3 BC = (Next - Current).Normalized();

                Vector3 Miter = new Vector3((AB + BC).Normalized().Xy.PerpendicularLeft.X,
                                            (AB + BC).Normalized().Xy.PerpendicularLeft.Y,
                                            (AB + BC).Normalized().Z);

                Vector3 Normal = new Vector3(-AB.Y, AB.X, AB.Z);

                float MiterLength = 1.0f / Vector3.Dot(Miter, Normal);

                RenderableVertexData.Add(new RenderableVertex(Previous, Current, Next, Color, -MiterLength));
                RenderableVertexData.Add(new RenderableVertex(Previous, Current, Next, Color, MiterLength));
            }
        }

        VertexData = RenderableVertexData.ToArray();
        SegmentData = RenderableSegmentData.ToArray();

        VertexBuffer = GL.GenBuffer();
        VertexArray = GL.GenVertexArray();

        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Marshal.SizeOf<RenderableVertex>(), VertexData, BufferUsageHint.StaticDraw);

        ElementBuffer = new Tuple<int, int>(GL.GenBuffer(), SegmentData.Length);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer.Item1);
        GL.BufferData(BufferTarget.ElementArrayBuffer, ElementBuffer.Item2 * sizeof(uint), SegmentData, BufferUsageHint.StaticDraw);
    }

    public void Render(Vector2 WindowSize, Camera Camera)
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Marshal.SizeOf<RenderableVertex>(), VertexData, BufferUsageHint.StaticDraw);

        RenderableShader.UseShader();

        GL.BindVertexArray(VertexArray);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false,
                               Marshal.SizeOf<RenderableVertex>(),
                               Marshal.OffsetOf<RenderableVertex>("Previous"));
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false,
                               Marshal.SizeOf<RenderableVertex>(),
                               Marshal.OffsetOf<RenderableVertex>("Current"));
        GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false,
                               Marshal.SizeOf<RenderableVertex>(),
                               Marshal.OffsetOf<RenderableVertex>("Next"));
        GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false,
                               Marshal.SizeOf<RenderableVertex>(),
                               Marshal.OffsetOf<RenderableVertex>("Color"));
        GL.VertexAttribPointer(4, 1, VertexAttribPointerType.Float, false,
                               Marshal.SizeOf<RenderableVertex>(),
                               Marshal.OffsetOf<RenderableVertex>("MiterLength"));

        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);
        GL.EnableVertexAttribArray(2);
        GL.EnableVertexAttribArray(3);
        GL.EnableVertexAttribArray(4);

        GL.Disable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);

        Matrix4 MVP = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-90.0f)) * Camera.GetCameraView() *
                      Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75.0f), WindowSize.X / WindowSize.Y, 0.1f, 7000.0f);
        
        RenderableShader.SetMatrix4Uniform("uMVP", MVP);
        RenderableShader.SetVector2Uniform("uScreenSize", WindowSize);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer.Item1);
        //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        GL.DrawElements(PrimitiveType.Triangles, ElementBuffer.Item2, DrawElementsType.UnsignedInt, 0);

        GL.Disable(EnableCap.Blend);
        GL.Enable(EnableCap.DepthTest);
    }

    public void Destroy()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.UseProgram(0);

        GL.DeleteBuffer(VertexBuffer);
        GL.DeleteVertexArray(VertexArray);

        GL.DeleteBuffer(ElementBuffer.Item1);

        GL.DeleteProgram(RenderableShader.GetShaderHandle());
    }
}
