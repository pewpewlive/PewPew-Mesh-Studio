using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace PewPewMeshStudio.Editor.Gizmos;

public class MoveGIZ : Gizmos
{
    public bool enabled;

    public static Vector3 MoveVertexes(Vector3[] verts) => new Vector3();

    public static Vector3 MoveObject(float[] moveVec, Vector3 objPos, Vector2 mouseDelta) // temp
    {
        //Vector3 move;

        return new Vector3();
    }
    public override void ToolAction()
    {
        throw new NotImplementedException();
    }
}
