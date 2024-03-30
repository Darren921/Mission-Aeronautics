using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class PlayerSoundManager
{
    public static void SaveLevelData(PlayerSoundData playerData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveVolumeData.save");
        binaryFormatter.Serialize(file, playerData);
        file.Close();
    }

    public static PlayerSoundData LoadGameState()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveVolumeData.save"))
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SaveVolumeData.save", FileMode.Open);
                PlayerSoundData playerData = (PlayerSoundData)binaryFormatter.Deserialize(file);
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
