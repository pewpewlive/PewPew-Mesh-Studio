using System.Collections.Generic; //беброчка смачна
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class EditingMesh // i will change the name
{
    public static Vector2i clientWindowSize = new Vector2i();

    private List<Mesh> meshes = new List<Mesh>();
    
    private List<VertexShower> viewVertices = new List<VertexShower>();
    private List<VertexShower> selectedVertices = new List<VertexShower>();

    public static Action OnMeshDestroy;
    public static Action OnMeshUpdate;

    private VertexShower vertexShower;

    public void FrameUpdate()
    {
        vertexShower.UpdateVertsPositions();
        OnMeshUpdate?.Invoke();
    }

    public void FrameUnload()
    {
        OnMeshDestroy?.Invoke();
    }

    public void LoadMesh(string loadMeshPath)
    {
        Mesh mesh = new Mesh(loadMeshPath, 1, new Vector3());
        meshes.Add(mesh);
        vertexShower = new VertexShower(mesh.vertices);
    }
}
