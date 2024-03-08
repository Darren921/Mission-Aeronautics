using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public static int CharacterChossen;

    private PlayerData playerData = new PlayerData();


    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
        Time.timeScale = 1.0f;

    }

    public void BackToSelection() 
    {
        SceneManager.LoadScene("ChooseCharacter");
    }


    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene("ChooseCharacter");
        Time.timeScale = 1.0f;

        playerData.levelTwoActive = false;
        playerData.levelThreeActive = false;
        playerData.levelFourActive = false;
        playerData.levelFiveActive = false;

        string json = JsonUtility.ToJson(playerData);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void ChoosenCharacter1()
    {
        CharacterChossen = 1;
        SceneManager.LoadScene("Levels");
        Time.timeScale = 1.0f;

    }
    public void ChoosenCharacter2()
    {
        CharacterChossen = 2;
        SceneManager.LoadScene("Levels");
        Time.timeScale = 1.0f;

    }
    public void ChoosenCharacter3()
    {
        CharacterChossen = 3;
        SceneManager.LoadScene("Levels");
        Time.timeScale = 1.0f;

    }
    public void ChoosenCharacter4()
    {
        CharacterChossen = 4;
        SceneManager.LoadScene("Levels");
        Time.timeScale = 1.0f;

    }

    /*
public void loadMultiPlayer()
{
SceneManager.LoadScene("ChooseCharacter");
}
*/
}
