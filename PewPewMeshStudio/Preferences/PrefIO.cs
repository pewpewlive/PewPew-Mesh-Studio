using Serilog;
using System.Text.Json;

namespace PewPewMeshStudio.Preferences;

/// <summary>
/// A class for working with preferences saving/reading.
/// </summary>
public static class PrefIO
{
    /// <summary>
    /// Saves Prefs object to a JSON file.
    /// </summary>
    /// <param name="path">A string containing a path.</param>
    /// <param name="prefs">A Prefs object to serialize.</param>
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

    /// <summary>
    /// Loads Prefs object from a JSON file.
    /// </summary>
    /// <param name="path">A string containing a path.</param>
    /// <returns>Prefs object or null if error is encountered.</returns>
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
