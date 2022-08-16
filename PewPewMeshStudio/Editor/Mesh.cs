using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class Mesh
{
    private Renderable mesh;
    public List<MeshVertex> vertices = new List<MeshVertex>();
    //private List<MeshVertex> vertsWorldPos = new List<MeshVertex>();

    public Vector3 position;
    //public Vector3 newPos;

    public Mesh(string loadMeshPath, int meshIndex, Vector3 position)
    {
        EditingMesh.OnMeshDestroy += DestroyMesh;
        EditingMesh.OnMeshUpdate += UpdateMesh;

        mesh = MeshParser.ParseMeshFile(loadMeshPath, meshIndex);
        mesh.GetVertexesData(ref vertices);
        //mesh.GetVertexesData(ref vertsWorldPos);
        this.position = position;
    }
    public Mesh(int MeshIndex, string meshString, Vector3 position)
    {
        EditingMesh.OnMeshDestroy += DestroyMesh;
        EditingMesh.OnMeshUpdate += UpdateMesh;

        mesh = MeshParser.ParseMeshString(meshString, MeshIndex);
        mesh.GetVertexesData(ref vertices);
        //mesh.GetVertexesData(ref vertsWorldPos);
        this.position = position;
    }
    public Mesh(Renderable meshCode, Vector3 position)
    {
        EditingMesh.OnMeshDestroy += DestroyMesh;
        EditingMesh.OnMeshUpdate += UpdateMesh;

        mesh = meshCode;
        mesh.GetVertexesData(ref vertices);
        //mesh.GetVertexesData(ref vertsWorldPos);
        this.position = position;
    }

    private void UpdateMesh()
    {
        mesh.Render(/*windowSize, meshCam*/);
    }
    
    private void UpdateMeshPosition(Vector3 newPos)
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            MeshVertex vert = vertices[i];
            vert.Position -= position;
            vertices[i] = vert;

            vert = vertices[i];
            vert.Position += newPos;
            vertices[i] = vert;
        }
        position = newPos;

        mesh.SetVertexesData(vertices.ToArray());
    }

    public void SetUpdate() => UI.Windows.InspectorWindow.OnObjectPositionUpdate += UpdateMeshPosition;
    public void RemoveUpdate() => UI.Windows.InspectorWindow.OnObjectPositionUpdate -= UpdateMeshPosition;

    public void DestroyMesh() => mesh.Destroy();
}
