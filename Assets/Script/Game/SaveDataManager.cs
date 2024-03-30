using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveDataManager
{
    public static void SaveLevelData(PlayerData playerData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.save");
        binaryFormatter.Serialize(file, playerData);
        file.Close();
    }

    public static PlayerData LoadGameState()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.save"))
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SaveData.save", FileMode.Open);
                PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(file);
                file.Close();
                return playerData;
            }
            catch
            {
                
            }
        }
        return null;
    }
}
