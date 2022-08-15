using System.IO;
using Newtonsoft.Json;

namespace PewPewMeshStudio.Preferences;

public class Prefs
{
    public int PrefsVersion { get; set; }
    public string Language { get; set; }
    public string Font { get; set; }
    public float Scale { get; set; }
    public Version OpenGlVersion { get; set; }
    public string Theme { get; set; }
    public bool ShowLastAction { get; set; }
    public bool ShowConsole { get; set; }
}
