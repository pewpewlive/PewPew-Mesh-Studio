using System.Collections.Generic; //беброчка смачна
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class EditingMesh // i will change the name
{
    // things
    public Vector2 mouseDelta;

    // scene
    //public Vector3 objectPosition = new Vector3();
    public List<Mesh> meshes { get; private set; } = new List<Mesh>();
    public int selectedMesh; // idk

    //private List<VertexShower> viewVertices = new List<VertexShower>();
    //private List<VertexShower> selectedVertices = new List<VertexShower>();

    public static Action OnMeshDestroy;
    public static Action OnMeshUpdate;

    private VertexShower vertexShower;

    public void FrameUpdate()
    {
        OnMeshUpdate?.Invoke();
        //vertexShower.UpdateVertsPositions();
        //vertexShower.GetNewPositions(meshes[0].vertices);
    }

    public void FrameLoad() 
    {
        LoadMesh("mesh.lua");
        SetEditingMesh(0);
    }

    public void FrameUnload()
    {
        OnMeshDestroy?.Invoke();
    }

    public void SetEditingMesh(int i) 
    {
        selectedMesh = i;

        foreach (Mesh mesh in meshes)
            mesh.selected = false;
        meshes[selectedMesh].selected = true;

        SetMeshUpdate();
        OnMeshSelect();
    }
    private void SetMeshUpdate()
    {
        foreach (Mesh mesh in meshes)
        {
            mesh.RemoveVertexUpdates();
            mesh.RemoveMeshPosUpdate();
        }

        meshes[selectedMesh].SetVertexUpdates();
        meshes[selectedMesh].SetMeshPosUpdate();
    }
    private void OnMeshSelect()
    {
        Random rand = new Random();

        meshes[selectedMesh].selectedVertsI.Clear();
        //meshes[selectedMesh].selectedVerts.Clear();
        for (int i = 0; i < 3; i++)
        {
            int rngIndex = rand.Next(0, meshes[selectedMesh].vertices.Count);

            meshes[selectedMesh].selectedVertsI.Add(rngIndex);
            //meshes[selectedMesh].selectedVerts.Add(meshes[selectedMesh].vertices[rngIndex]);
        }
    }

    public void SetMeshState(bool state, int i) => meshes[i].hidden = state;

    public void LoadMesh(string loadMeshPath)
    {
        Mesh mesh = new Mesh(loadMeshPath, 1, new Vector3());
        meshes.Add(mesh);
        selectedMesh = 0; // for now
        SetMeshUpdate();
        //vertexShower = new VertexShower(mesh.vertices);
    }

    public void DeleteMesh(int index)
    {
        meshes[index].DestroyMesh();
        meshes.RemoveAt(index);
    }
}
