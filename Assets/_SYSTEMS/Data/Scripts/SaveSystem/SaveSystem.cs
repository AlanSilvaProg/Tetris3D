using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class DataToSave
{
    public List<PlayerStatsInfo> rankList;
}

namespace SaveSystem
{
    public static class SaveSystem<T>
    {
        static FileStream file = null;
        static BinaryFormatter bf = new BinaryFormatter();
        public static void SaveGame(T saveInfo,string destinationName)
        {
            file = File.Create(Application.persistentDataPath + destinationName);
            bf.Serialize(file, saveInfo);
            file.Close();
        }

        public static T LoadGameInfo(string destinationName)
        {
            file = File.Open(Application.persistentDataPath + destinationName, FileMode.Open);
            T data = (T)bf.Deserialize(file);
            file.Close();
            return data;
        }

        public static bool HasDataToLoad(string destinationName)
        {
            return File.Exists(Application.persistentDataPath + destinationName);
        }

        public static void RemoveAllDataSaved(string destinationName)
        {
            if (HasDataToLoad(destinationName))
                File.Delete(Application.persistentDataPath + destinationName);
        }
    }
}