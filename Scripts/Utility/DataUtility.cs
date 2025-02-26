using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TryliomUtility
{
    public static class DataUtility
    {
        public static void SaveData(ScriptableObject data, string suffix = "")
        {
            var bf = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + $"/{data.name}{suffix}.pso", FileMode.OpenOrCreate);
            var json = JsonUtility.ToJson(data);
                
            bf.Serialize(file, json);
            file.Close();
        }
        
        public static void LoadData(ScriptableObject data, string suffix = "")
        {
            var fileName = Application.persistentDataPath + $"/{data.name}{suffix}.pso";

            if (File.Exists(fileName))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(fileName, FileMode.Open);
                    
                JsonUtility.FromJsonOverwrite((string) bf.Deserialize(file), data);
                file.Close();
            }
            else
            {
                Debug.LogWarning("No persistent previous data");
            }
        }
        
        public static void DeleteData(ScriptableObject data, string suffix = "")
        {
            var path = Application.persistentDataPath + $"/{data.name}{suffix}.pso";
            
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        
        public static bool IsDataSaved(ScriptableObject data, string suffix = "")
        {
            return File.Exists(Application.persistentDataPath + $"/{data.name}{suffix}.pso");
        }
    }
}