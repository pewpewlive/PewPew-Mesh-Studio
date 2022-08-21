using PewPewMeshStudio.Renderer;
namespace PewPewMeshStudio.PPMP;

public class MeshProject
{
    public ushort PpmpRevision { get; set; }

    // TODO: encode mesh data directly into PPMP file
    public string[] currentMeshes { get; set; }
    public int[] currentMeshesIndexes { get; set; }
}
