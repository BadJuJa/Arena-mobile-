using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[DefaultExecutionOrder(-2)]
public class Settings_SaveLoad : MonoBehaviour {

    public static void Save(ActiveSetings settings) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        Settings setts = new Settings(settings);

        formatter.Serialize(stream, setts);
        stream.Close();
    }

    public static Settings Load() {
        string path = Application.persistentDataPath + "/settings.data";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Settings setts = formatter.Deserialize(stream) as Settings;
            stream.Close();
            return setts;
        } else {
            Settings setts = new Settings();
            return setts;
        }
    }
}