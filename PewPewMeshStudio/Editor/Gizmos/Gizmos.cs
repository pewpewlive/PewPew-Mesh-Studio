using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace PewPewMeshStudio.Editor.Gizmos;

public abstract class Gizmos
{
    public Vector3 toolAxisX = new Vector3(), toolAxisY = new Vector3(), toolAxisZ = new Vector3();

    public abstract void ToolAction();
}
