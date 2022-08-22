using Serilog;
using System.Runtime.Serialization;
using System.Xml;
//using System.Runtime.Serialization.Json;

namespace PewPewMeshStudio.PPMP;

/// <summary>
/// A class for working with .ppmp file saving/reading.
/// </summary>
public static class MeshProjectIO
{
    /// <summary>
    /// Saves MeshProject object to a .ppmp file.
    /// </summary>
    /// <param name="path">A string containing a path.</param>
    /// <param name="meshProject">A MeshProject object to serialize.</param>
    public static void Save(string path, MeshProject meshProject)
    {
        try
        {
            using FileStream writer = new FileStream(path, FileMode.Create);
            DataContractSerializer ser = new DataContractSerializer(typeof(MeshProject));
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MeshProject));

            ser.WriteObject(writer, meshProject);
            Log.Information("(MeshProjectIO @ Save) <{thread}> PPMP serialized to {path}", Thread.CurrentThread.Name, path);
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(MeshProjectIO @ Save) <{thread}> Error while serializing {path}", Thread.CurrentThread.Name, path);
        }
    }

    /// <summary>
    /// Loads MeshProject object from a .ppmp file.
    /// </summary>
    /// <param name="path">A string containing a path.</param>
    /// <returns>MeshProject object or null if error is encountered.</returns>
    public static MeshProject? Load(string path)
    {
        try
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            using XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(MeshProject));
            MeshProject? meshProject = (MeshProject?)ser.ReadObject(reader, true);
            Log.Information("(MeshProjectIO @ Save) <{thread}> PPMP deserialized from {path}", Thread.CurrentThread.Name, path);
            Log.Debug("{@0}", meshProject);
            return meshProject;
        }
        catch (Exception Ex)
        {
            Log.Error(Ex, "(MeshProjectIO @ Load) <{thread}> Error while deserializing {path}", Thread.CurrentThread.Name, path);
            return null;
        }
    }
}
