using Serilog;
using System.Text.Json;
using System.Threading.Tasks;

namespace PewPewMeshStudio.PPMP;

public class MeshProjectIO
{
    public static MeshProject? Load(string path)
    {
        MeshProject? project = JsonSerializer.Deserialize<MeshProject>(File.ReadAllBytes(path));
        Log.Debug("{@prefs}", project);
        return project;
    }
    public static void Save(string path, MeshProject meshProject)
    {
        // Remove WriteIndented later-on to save space
        File.WriteAllBytes(path, JsonSerializer.SerializeToUtf8Bytes(meshProject, new JsonSerializerOptions { WriteIndented = true }));
    }
}
