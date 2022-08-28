using System.Collections.Generic;
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using PewPewMeshStudio.Core;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class VertexShower //  i will change the name
{
    private List<MeshVertex> vertices = new List<MeshVertex>();
    private List<Mesh> vertsPreview = new List<Mesh>();
    private const string MeshyMcMeshface = "meshes={{vertexes={{0,0,-0.1},{0.072,-0.053,-0.045},{-0.028,-0.085,-0.045},{-0.089,0,-0.045},{-0.028,0.085,-0.045},{0.072,0.053,-0.045},{0.028,-0.085,0.045},{-0.072,-0.053,0.045},{-0.072,0.053,0.045},{0.028,0.085,0.045},{0.089,0,0.045},{0,0,0.1}},segments={{3,0},{2,3},{4,0},{3,4},{4,5},{10,1},{5,10},{6,2},{1,6},{7,3},{2,7},{8,4},{3,8},{9,5},{4,9},{10,6},{6,7},{7,8},{8,9},{9,10},{11,6},{2,0},{10,11},{0,1},{11,7},{1,2},{11,8},{5,1},{11,9},{0,5}}}}";

    public VertexShower(List<MeshVertex> verts)
    {
        vertices = verts;

        for (int i = 0; i < vertices.Count; i++)
            vertsPreview.Add(new Mesh(1, MeshyMcMeshface, vertices[i].Position));
    }
    public void UpdateVertsPositions()
    {
        //for (int i = 0; i < vertices.Count; i++)
        //    vertsPreview[i].UpdateMeshPosition(vertices[i].Position);
    }

    public void GetNewPositions(List<MeshVertex> verts) => vertices = verts;
}
