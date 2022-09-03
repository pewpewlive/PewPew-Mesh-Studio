using OpenTK.Mathematics;
using PewPewMeshStudio.Renderer;

namespace PewPewMeshStudio.ExtraUtils;

class VectorUtils
{
    public static Vector3 CenterOfVectors(List<Vector3> vectors)
    {
        Vector3 sum = new Vector3();
        if (vectors == null || vectors.Count == 0)
            return sum;

        foreach (Vector3 vec in vectors)
            sum += vec;
        return sum / vectors.Count;
    }
    /*public static Vector3 CenterOfVectors(List<MeshVertex> vectors)
    {
        Vector3 sum = new Vector3();
        if (vectors == null || vectors.Count == 0)
            return sum;

        foreach (MeshVertex vec in vectors)
            sum += vec.Position;
        return sum / vectors.Count;
    }*/
}
