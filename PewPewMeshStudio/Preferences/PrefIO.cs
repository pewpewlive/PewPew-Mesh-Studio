using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PewPewMeshStudio.Preferences;

public static class PrefIO
{
    public static Prefs? Load(string path)
    {
        Prefs? preferences = JsonSerializer.Deserialize<Prefs>(File.ReadAllBytes(path));
        Log.Debug("{@prefs}", preferences);
        return preferences;
    }
    public static void Save(string path, Prefs prefs)
    {
        File.WriteAllBytes(path, JsonSerializer.SerializeToUtf8Bytes(prefs, new JsonSerializerOptions { WriteIndented = true }));
    }
}
