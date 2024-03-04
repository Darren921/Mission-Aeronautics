using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPick : MonoBehaviour
{
    public static int LevelChossen;

    public bool levelTwoActive;
    public bool levelThreeActive;
    public bool levelFourActive;
    public bool levelFiveActive;

    public Image[] locks;

    public Image[] levels;
    public Sprite[] levelSprites;

    [SerializeField] private AnimatorOverrideController animatorOverrideController;

    private void Update()
    {
        if (levelTwoActive)
        {
            locks[0].GetComponent<Animator>().SetBool("locked", false);
            levels[0].sprite = levelSprites[0];
        }

        if (levelThreeActive)
        {
            locks[1].GetComponent<Animator>().SetBool("locked", false);
            levels[1].sprite = levelSprites[1];
        }

        if(levelFourActive)
        {
            locks[2].GetComponent<Animator>().SetBool("locked", false);
            levels[2].sprite = levelSprites[2];
        }

        if(levelFiveActive)
        {
            locks[3].GetComponent<Animator>().SetBool("locked", false);
            levels[3].sprite = levelSprites[3];
        }
    }


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
        if (levelTwoActive)
        {
            LevelChossen = 2;
            SceneManager.LoadScene("MainGame");
        }
    }
    public void Level3()
    {
        if (levelThreeActive)
        {
            LevelChossen = 3;
            SceneManager.LoadScene("MainGame");
        }
    }
    public void Level4()
    {
        if(levelFourActive)
        {
            LevelChossen = 4;
            SceneManager.LoadScene("MainGame");
        }
    }

    public void Level5()
    {
        if (levelFiveActive) 
        {
            LevelChossen = 5;
            SceneManager.LoadScene("MainGame");
        }
    }

    public int Level()
    {
        return LevelChossen;
    }
}
