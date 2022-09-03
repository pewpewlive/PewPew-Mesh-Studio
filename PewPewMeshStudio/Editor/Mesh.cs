using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class Mesh
{
    private Renderable mesh;
    public List<MeshVertex> vertices = new List<MeshVertex>();

    public List<int> selectedVertsI = new List<int>();
    //public List<MeshVertex> selectedVerts = new List<MeshVertex>();
    
    public bool selected = false;
    public bool hidden = false;
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
        if (hidden == false)
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

    public void UpdateVertexPosition(Vector3 newPos)
    {
        Vector3 center = GetVerticesCenterPosition();

        foreach (int selected in selectedVertsI)
        {
            MeshVertex vert = vertices[selected];
            vert.Position -= center;
            vertices[selected] = vert;

            vert = vertices[selected];
            vert.Position += newPos;
            vertices[selected] = vert;
        }

        mesh.SetVertexesData(vertices.ToArray());
    }
    public Vector3 GetVerticesCenterPosition()
    {
        List<Vector3> vertPositions = new List<Vector3>();

        foreach (int selectedVert in selectedVertsI)
            vertPositions.Add(vertices[selectedVert].Position);

        return ExtraUtils.VectorUtils.CenterOfVectors(vertPositions);
    }

    public void UpdateVertexColor(Vector4 newColor)
    {
        foreach (int selected in selectedVertsI)
        {
            MeshVertex vert = vertices[selected];
            vert.Color = newColor;
            vertices[selected] = vert;

            vert = vertices[selected];
            vert.Color += newColor;
            vertices[selected] = vert;
        }

        mesh.SetVertexesData(vertices.ToArray());
    }

    public void SetMeshPosUpdate() => UI.Windows.InspectorWindow.OnMeshPositionUpdate += UpdateMeshPosition;
    public void RemoveMeshPosUpdate() => UI.Windows.InspectorWindow.OnMeshPositionUpdate -= UpdateMeshPosition;

    public void SetVertexUpdates()
    {
        UI.Windows.InspectorWindow.OnVertexPositionUpdate += UpdateVertexPosition;
        UI.Windows.InspectorWindow.OnVertexColorUpdate += UpdateVertexColor;
    }
    public void RemoveVertexUpdates()
    {
        UI.Windows.InspectorWindow.OnVertexPositionUpdate -= UpdateVertexPosition;
        UI.Windows.InspectorWindow.OnVertexColorUpdate -= UpdateVertexColor;
    }

    public void DestroyMesh()
    {
        RemoveVertexUpdates();
        RemoveMeshPosUpdate();
        EditingMesh.OnMeshDestroy -= DestroyMesh;
        EditingMesh.OnMeshUpdate -= UpdateMesh;
        mesh.Destroy();
    }
}