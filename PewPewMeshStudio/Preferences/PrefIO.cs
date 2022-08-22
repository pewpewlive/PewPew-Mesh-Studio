using Serilog;
using System.Text.Json;

namespace PewPewMeshStudio.Preferences;

public static class PrefIO
{
    public static void Save(string path, Prefs prefs)
    {
        try 
        { 
            File.WriteAllBytes(path, JsonSerializer.SerializeToUtf8Bytes(prefs, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(PrefIO @ Save) <{thread}> Error while writing to {path}", Thread.CurrentThread.Name, path);
        }
    }

    public static Prefs? Load(string path)
    {
        try
        {
            Prefs? preferences = JsonSerializer.Deserialize<Prefs>(File.ReadAllBytes(path));

            Log.Debug("{@0}", preferences);
            return preferences;
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(PrefIO @ Load) <{thread}> Error while parsing {path}", Thread.CurrentThread.Name, path);
            return null;
        }
    }
}
