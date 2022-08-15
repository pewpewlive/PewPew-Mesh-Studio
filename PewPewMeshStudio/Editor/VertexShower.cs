using System.Collections.Generic;
using PewPewMeshStudio.Renderer;
using PewPewMeshStudio.LuaUtils;
using PewPewMeshStudio.Core;
using OpenTK.Mathematics;

namespace PewPewMeshStudio.Editor;

public class ViewVertices //  i will change the name
{
    private List<MeshVertex> vertices = new List<MeshVertex>();
    private List<Mesh> vertsPreview = new List<Mesh>();

    public ViewVertices(List<MeshVertex> verts)
    {
        vertices = verts;

        for (int i = 0; i < vertices.Count; i++)
            vertsPreview.Add(new Mesh("vertPickSphere.lua", vertices[i].Position));
    }

    public void UpdateVertsPositions()
    {
        for (int i = 0; i < vertices.Count; i++)
            vertsPreview[i].position = vertices[i].Position; 
    }

    public void GetNewPositions(List<MeshVertex> verts)
    {
        vertices = verts;

        for (int i = 0; i < vertices.Count; i++)
            vertsPreview.Add(new Mesh("vertPickSphere.lua", vertices[i].Position));
    }
}
