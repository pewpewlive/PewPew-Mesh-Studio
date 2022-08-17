using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Renderer;

public class Shader
{
    private int ShaderHandle;

    private int VertexShader;
    private int FragmentShader;

    public Shader()
    {
        VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, Properties.Resources.VertShader);

        FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, Properties.Resources.FragShader);

        GL.CompileShader(VertexShader);
        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int VertexShaderStatus);
        if (VertexShaderStatus == 0)
        {
            Console.WriteLine(string.Format("Vertex Shader {0}", GL.GetShaderInfoLog(VertexShader)));
            Console.Read();
        }

        GL.CompileShader(FragmentShader);
        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int FragmentShaderStatus);
        if (FragmentShaderStatus == 0)
        {
            Console.WriteLine(string.Format("Fragment Shader {0}", GL.GetShaderInfoLog(FragmentShader)));
            Console.Read();
        }

        ShaderHandle = GL.CreateProgram();

        GL.AttachShader(ShaderHandle, VertexShader);
        GL.AttachShader(ShaderHandle, FragmentShader);

        GL.LinkProgram(ShaderHandle);
        GL.GetProgram(ShaderHandle, GetProgramParameterName.LinkStatus, out int ShaderHandleStatus);
        if (ShaderHandleStatus == 0)
        {
            Console.WriteLine(string.Format("Shader Linking error: {0}", GL.GetProgramInfoLog(ShaderHandle)));
            Console.Read();
        }

        GL.DetachShader(ShaderHandle, VertexShader);
        GL.DetachShader(ShaderHandle, FragmentShader);
        GL.DeleteShader(FragmentShader);
        GL.DeleteShader(VertexShader);
    }

    public void UseShader() => GL.UseProgram(ShaderHandle);

    public int GetShaderHandle() => ShaderHandle;

    public void SetMatrix4Uniform(string Name, Matrix4 U) => GL.UniformMatrix4(GL.GetUniformLocation(GetShaderHandle(), Name), true, ref U);

    public void SetMatrix3Uniform(string Name, Matrix3 U) => GL.UniformMatrix3(GL.GetUniformLocation(GetShaderHandle(), Name), true, ref U);

    public void SetVector3Uniform(string Name, Vector3 U) => GL.Uniform3(GL.GetUniformLocation(GetShaderHandle(), Name), U);

    public void SetVector2Uniform(string Name, Vector2 U) => GL.Uniform2(GL.GetUniformLocation(GetShaderHandle(), Name), U);
}
