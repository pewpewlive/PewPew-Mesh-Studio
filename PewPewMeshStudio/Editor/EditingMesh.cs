using System.Collections.Generic; //беброчка смачна
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class EditingMesh // i will change the name
{
    public static Vector2i clientWindowSize = new Vector2i();

    private List<Mesh> meshes = new List<Mesh>();
    public int currentEditingMesh; // idk
    
    private List<VertexShower> viewVertices = new List<VertexShower>();
    private List<VertexShower> selectedVertices = new List<VertexShower>();

    public static Action OnMeshDestroy;
    public static Action OnMeshUpdate;

    private VertexShower vertexShower;

    public void FrameUpdate()
    {
        OnMeshUpdate?.Invoke();
        //vertexShower.UpdateVertsPositions();
        //vertexShower.GetNewPositions(meshes[0].vertices);
    }

    public void FrameUnload()
    {
        OnMeshDestroy?.Invoke();
    }

    public void SetStuffUpdating()
    {
        foreach (Mesh mesh in meshes)
            mesh.RemoveUpdate();

        meshes[currentEditingMesh].SetUpdate();
    }

    public void LoadMesh(string loadMeshPath)
    {
        Mesh mesh = new Mesh(loadMeshPath, 1, new Vector3());
        meshes.Add(mesh);
        currentEditingMesh = 0; // for now
        SetStuffUpdating();
        //vertexShower = new VertexShower(mesh.vertices);
    }
}
