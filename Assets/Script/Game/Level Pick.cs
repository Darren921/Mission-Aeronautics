using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPick : MonoBehaviour
{
    public static int LevelChossen;


    public void BackToCharacter()
    {
        SceneManager.LoadScene("ChooseCharacter");
        Time.timeScale = 1.0f;

    }

    public void Level1()
    {
        LevelChossen = 1;
        SceneManager.LoadScene("MainGame");
    }
    public void Level2()
    {
        LevelChossen = 2;
        SceneManager.LoadScene("MainGame");
    }
    public void Level3()
    {
        LevelChossen = 3;
        SceneManager.LoadScene("MainGame");
    }
    public void Level4()
    {
        LevelChossen = 4;
        SceneManager.LoadScene("MainGame");
    }

    public void Level5()
    {
        LevelChossen = 5;
        SceneManager.LoadScene("MainGame");
    }
}
