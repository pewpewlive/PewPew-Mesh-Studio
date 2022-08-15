using System.Collections.Generic; //беброчка смачна
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class EditingMesh // i will change the name
{
    public static Vector2i clientWindowSize = new Vector2i();

    private List<Mesh> meshes = new List<Mesh>();
    
    private List<ViewVertices> viewVertices = new List<ViewVertices>();
    private List<ViewVertices> selectedVertices = new List<ViewVertices>();

    public static Action OnMeshDestroy;
    public static Action OnMeshUpdate;

    public void FrameUpdate()
    {
        OnMeshUpdate?.Invoke();
    }

    public void FrameUnload()
    {
        OnMeshDestroy?.Invoke();
    }

    public void LoadMesh(string loadMeshPath)
    {
        meshes.Add(new Mesh(loadMeshPath, new Vector3()));
    }
}
