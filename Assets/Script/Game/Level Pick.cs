using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPick : MonoBehaviour
{
    public static int LevelChossen;

    public Image[] locks;

    public Image[] levels;
    public Sprite[] levelSprites;

    [SerializeField] private AnimatorOverrideController animatorOverrideController;

    private PlayerData playerData;

    private string json;

    private PlayerData loadedData;

    public bool levelTwoActive;
    public bool levelThreeActive;
    public bool levelFourActive;
    public bool levelFiveActive;

    private void Start()
    {

        playerData = new PlayerData();
        json = System.IO.File.ReadAllText(Application.persistentDataPath + "/savefile.json");
        loadedData = JsonUtility.FromJson<PlayerData>(json);
        levelTwoActive = loadedData.levelTwoActive;
        levelThreeActive = loadedData.levelThreeActive;
        levelFourActive = loadedData.levelFourActive;
        levelFiveActive = loadedData.levelFiveActive;
    }

    private void Update()
    {
        if (levelTwoActive)
        {
            locks[0].GetComponent<Animator>().SetBool("locked", false);
            levels[0].sprite = levelSprites[0];
        }

        if (levelThreeActive)
        {
            levelTwoActive = true;
            locks[0].GetComponent<Animator>().SetBool("locked", false);
            levels[0].sprite = levelSprites[0];

            locks[1].GetComponent<Animator>().SetBool("locked", false);
            levels[1].sprite = levelSprites[1];
        }

        if(levelFourActive)
        {
            levelTwoActive = true;
            locks[0].GetComponent<Animator>().SetBool("locked", false);
            levels[0].sprite = levelSprites[0];

            levelThreeActive = true;
            locks[1].GetComponent<Animator>().SetBool("locked", false);
            levels[1].sprite = levelSprites[1];

            locks[2].GetComponent<Animator>().SetBool("locked", false);
            levels[2].sprite = levelSprites[2];
        }

        if(levelFiveActive)
        {
            levelTwoActive = true;
            locks[0].GetComponent<Animator>().SetBool("locked", false);
            levels[0].sprite = levelSprites[0];

            levelThreeActive = true;
            locks[1].GetComponent<Animator>().SetBool("locked", false);
            levels[1].sprite = levelSprites[1];

            levelFourActive = true;
            locks[2].GetComponent<Animator>().SetBool("locked", false);
            levels[2].sprite = levelSprites[2];

            locks[3].GetComponent<Animator>().SetBool("locked", false);
            levels[3].sprite = levelSprites[3];
        }
    }

    public void Reset()
    {
        levelTwoActive = false;
        levelThreeActive = false;
        levelFourActive = false;
        levelFiveActive = false;

        string json = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
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
        Time.timeScale = 1.0f;
        InputManager.DisableInGame();
    }
    public void Level2()
    {
        if (levelTwoActive)
        {
            LevelChossen = 2;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }
    public void Level3()
    {
        if (levelThreeActive)
        {
            LevelChossen = 3;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }
    public void Level4()
    {
        if(levelFourActive)
        {
            LevelChossen = 4;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }

    public void Level5()
    {
        if (levelFiveActive) 
        {
            LevelChossen = 5;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }

    public int Level()
    {
        return LevelChossen;
    }
}
