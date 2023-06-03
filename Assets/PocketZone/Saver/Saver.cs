using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Saver : ISaver
{
    public void Load<T>(string key, Action<T> callback)
    {
        var path = PathBuilder(key);
        if (File.Exists(path))
        {
            using (var fileStream = new StreamReader(path))
            {
                var jsonData = fileStream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(jsonData);

                if (data == null)
                    return;

                callback?.Invoke(data);
            }
        }
        else { Debug.Log($"SaveFile: {key} not found"); }
    }

    public void Save(string key, object data, Action callback = null)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        var path = PathBuilder(key);

        using (var fileStream = new StreamWriter(path))
        {
            fileStream.Write(jsonData);
        }

        callback?.Invoke();
    }

    private string PathBuilder(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}
