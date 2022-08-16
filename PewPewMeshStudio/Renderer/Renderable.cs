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
                if (i == 0)
                {
                    Vector3 NewCurrent = LineVertexData[Segment[i]].Position;
                    Vector3 NewNext = LineVertexData[Segment[i + 1]].Position;
                    Vector3 NewPrevious = NewCurrent - (NewNext - NewCurrent).Normalized();
                    RenderableVertexData.Add(new RenderableVertex(NewPrevious, NewCurrent, NewNext, LineVertexData[Segment[i]].Color, -1.0f));
                    RenderableVertexData.Add(new RenderableVertex(NewPrevious, NewCurrent, NewNext, LineVertexData[Segment[i]].Color, 1.0f));
                }
                else if (i >= 1 && i < Segment.Length - 1)
                {
                    Vector3 NewPrevious = LineVertexData[Segment[i - 1]].Position;
                    Vector3 NewCurrent = LineVertexData[Segment[i]].Position;
                    Vector3 NewNext = LineVertexData[Segment[i + 1]].Position;
                    RenderableVertexData.Add(new RenderableVertex(NewPrevious, NewCurrent, NewNext, LineVertexData[Segment[i]].Color, -1.0f));
                    RenderableVertexData.Add(new RenderableVertex(NewPrevious, NewCurrent, NewNext, LineVertexData[Segment[i]].Color, 1.0f));
                }
                else if (i == Segment.Length - 1)
                {
                    Vector3 NewPrevious = LineVertexData[Segment[i - 1]].Position;
                    Vector3 NewCurrent = LineVertexData[Segment[i]].Position;
                    Vector3 NewNext = NewCurrent + (NewCurrent - NewPrevious).Normalized();
                    RenderableVertexData.Add(new RenderableVertex(NewPrevious, NewCurrent, NewNext, LineVertexData[Segment[i]].Color, -1.0f));
                    RenderableVertexData.Add(new RenderableVertex(NewPrevious, NewCurrent, NewNext, LineVertexData[Segment[i]].Color, 1.0f));
                }

                RenderableSegmentData.AddRange(new uint[3] { Convert.ToUInt32(RenderableVertexData.Count - 2),
                                                             Convert.ToUInt32(RenderableVertexData.Count - 1),
                                                             Convert.ToUInt32(RenderableVertexData.Count) });

                RenderableSegmentData.AddRange(new uint[3] { Convert.ToUInt32(RenderableVertexData.Count - 1),
                                                             Convert.ToUInt32(RenderableVertexData.Count),
                                                             Convert.ToUInt32(RenderableVertexData.Count + 1) });
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
                               Marshal.OffsetOf<RenderableVertex>("Sign"));

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
