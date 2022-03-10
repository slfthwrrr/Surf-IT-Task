using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static void SaveControls(DropboxHandler dropdown)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/conrols.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        ControlsData data = new ControlsData(dropdown);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ControlsData LoadControls()
    {
        string path = Application.persistentDataPath + "/conrols.dat";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ControlsData data = formatter.Deserialize(stream) as ControlsData;
            stream.Close();
            
            return data;            
        }
        else
        {
            Debug.Log("Save file does not exist in " + path);
            return null;
        }
    }
}
