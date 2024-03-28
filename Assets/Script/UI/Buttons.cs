using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public static int CharacterChossen;
    private PlayerData playerData;
    private PlayerData loadedData;

    public void BackToTitle()
    {
        Time.timeScale = 1.0f;
        StopAllCoroutines();
        SceneManager.LoadScene("TitleScreen");
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
        if(Tutorial.tutFin != true)
        {
            SceneManager.LoadScene("Tut");
            Time.timeScale = 1.0f;
            LevelPick.LevelChossen = 0;
           
        }
        else
        {
            SceneManager.LoadScene("Levels");
            Time.timeScale = 1.0f;
           
        }
       

    }
    public void ChoosenCharacter2()
    {
        CharacterChossen = 2;
        if (Tutorial.tutFin != true)
        {
            SceneManager.LoadScene("Tut");
            Time.timeScale = 1.0f;
            LevelPick.LevelChossen = 0;
            
        }
        else
        {
            SceneManager.LoadScene("Levels");
            Time.timeScale = 1.0f;
           
        }
       
    }
    public void ChoosenCharacter3()
    {
        CharacterChossen = 3;
        if (Tutorial.tutFin != true)
        {
            SceneManager.LoadScene("Tut");
            Time.timeScale = 1.0f;
            LevelPick.LevelChossen = 0;
            

        }
        else
        {
            SceneManager.LoadScene("Levels");
            Time.timeScale = 1.0f;
           
        }

    }
    public void ChoosenCharacter4()
    {
        CharacterChossen = 4;
        if (Tutorial.tutFin != true)
        {
            SceneManager.LoadScene("Tut");
            Time.timeScale = 1.0f;
            LevelPick.LevelChossen = 0;
           

        }
        else
        {
            SceneManager.LoadScene("Levels");
            Time.timeScale = 1.0f;
           
        }
    }
}
