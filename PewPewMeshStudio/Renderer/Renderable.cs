﻿using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;
namespace PewPewMeshStudio.Renderer;
public class Renderable
{
    private int VertexBuffer;
    private int VertexArray;
    private List<Tuple<int, int>> ElementBuffers = new List<Tuple<int, int>>();

    private Shader RenderableShader = new Shader(); // FIXME: prints gl shader errors

    private MeshVertex[] VertexData;

    public Renderable(MeshVertex[] LineVertexData, uint[][] Segments)
    {
        VertexData = LineVertexData;

        VertexBuffer = GL.GenBuffer();
        VertexArray = GL.GenVertexArray();

        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Marshal.SizeOf<MeshVertex>(), VertexData, BufferUsageHint.StaticDraw);

        foreach (uint[] Segment in Segments)
        {
            ElementBuffers.Add(new Tuple<int, int>(GL.GenBuffer(), Segment.Length));
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffers.Last().Item1);
            GL.BufferData(BufferTarget.ElementArrayBuffer, ElementBuffers.Last().Item2 * sizeof(uint), Segment, BufferUsageHint.StaticDraw);
        }
    }

    public void Render(/*OpenTK.Mathematics.Vector2 WindowSize, Camera Camera*/)
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Marshal.SizeOf<MeshVertex>(), VertexData, BufferUsageHint.StaticDraw);

        RenderableShader.UseShader();

        GL.BindVertexArray(VertexArray);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false,
                               Marshal.SizeOf<MeshVertex>(),
                               Marshal.OffsetOf<MeshVertex>("Position"));
        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false,
                               Marshal.SizeOf<MeshVertex>(),
                               Marshal.OffsetOf<MeshVertex>("Color"));
        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);
        GL.Disable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);

        OpenTK.Mathematics.Matrix4 MVP =
            OpenTK.Mathematics.Matrix4.CreateRotationX(OpenTK.Mathematics.MathHelper.DegreesToRadians(-90.0f)) * Core.Window.MeshCamera.GetCameraView() * OpenTK.Mathematics.Matrix4.CreatePerspectiveFieldOfView(OpenTK.Mathematics.MathHelper.DegreesToRadians(75.0f), Core.Window.windowSize.X / Core.Window.windowSize.Y, 0.1f, 7000.0f);

        RenderableShader.SetMatrix4Uniform("uMVP", MVP);
        RenderableShader.SetVector2Uniform("uScreenSize", Core.Window.windowSize);

        foreach (Tuple<int, int> ElementBuffer in ElementBuffers)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer.Item1);
            GL.DrawElements(PrimitiveType.LineStrip, ElementBuffer.Item2, DrawElementsType.UnsignedInt, 0); // FIXME: offending line of code
        }
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
        ElementBuffers.Clear();

        GL.DeleteProgram(RenderableShader.GetShaderHandle());
    }


    public void GetVertexesData(ref List<MeshVertex> verts)
    {
        for (int i = 0; i < VertexData.Length; i++)
            verts.Add(VertexData[i]);
    }
    public void SetVertexesData(MeshVertex[] data) => VertexData = data;
}