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
    public Sprite[] darkSprites;

    [SerializeField] private AnimatorOverrideController animatorOverrideController;

    public int levelTwoState;
    public int levelThreeState;
    public int levelFourState;
    public int levelFiveState;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        PlayerData playerData = SaveDataManager.LoadGameState();
        if (playerData != null )
        {
            levelTwoState = playerData.levelTwoState;
            levelThreeState = playerData.levelThreeState;
            levelFourState = playerData.levelFourState;
            levelFiveState = playerData.levelFiveState;
        }
    }

    public void ResetData()
    {
        PlayerData playerData = new PlayerData();

        playerData.levelTwoState = 0;
        playerData.levelThreeState = 0;
        playerData.levelFourState = 0;
        playerData.levelFiveState = 0;

        SaveDataManager.SaveLevelData(playerData);

        LoadData();
    }

    private void Update()
    {
        if (levelTwoState == 1)
        {
            locks[0].GetComponent<Animator>().SetInteger("locked", 1);
            levels[0].sprite = levelSprites[0];

            PlayerData playerData = new PlayerData();
            PlayerData savedData = SaveDataManager.LoadGameState();

            if (savedData != null)
            {
                levelTwoState = savedData.levelTwoState;
                levelThreeState = savedData.levelThreeState;
                levelFourState = savedData.levelFourState;
                levelFiveState = savedData.levelFiveState;
            }

            playerData.levelTwoState = 2;
            playerData.levelThreeState = levelThreeState;
            playerData.levelFourState = levelFourState;
            playerData.levelFiveState = levelFiveState;

            SaveDataManager.SaveLevelData(playerData);
        }
        else if (levelTwoState == 2)
        {
            levels[0].sprite = levelSprites[0];
            locks[0].GetComponent<Animator>().SetInteger("locked", 2);
        }
        else if (levelTwoState == 0)
        {
            levels[0].sprite = darkSprites[0];
            locks[0].GetComponent<Animator>().SetInteger("locked", 0);
        }

        if (levelThreeState == 1)
        {
            locks[1].GetComponent<Animator>().SetInteger("locked", 1);
            levels[1].sprite = levelSprites[1];

            PlayerData playerData = new PlayerData();
            PlayerData savedData = SaveDataManager.LoadGameState();

            if (savedData != null)
            {
                levelTwoState = savedData.levelTwoState;
                levelThreeState = savedData.levelThreeState;
                levelFourState = savedData.levelFourState;
                levelFiveState = savedData.levelFiveState;
            }

            playerData.levelTwoState = levelTwoState;
            playerData.levelThreeState = 2;
            playerData.levelFourState = levelFourState;
            playerData.levelFiveState = levelFiveState;

            SaveDataManager.SaveLevelData(playerData);
        }
        else if (levelThreeState == 2)
        {
            levels[1].sprite = levelSprites[1];
            locks[1].GetComponent<Animator>().SetInteger("locked", 2);
        }
        else if (levelThreeState == 0)
        {
            levels[1].sprite = darkSprites[1];
            locks[1].GetComponent<Animator>().SetInteger("locked", 0);
        }

        if (levelFourState == 1)
        {
            locks[2].GetComponent<Animator>().SetInteger("locked", 1);
            levels[2].sprite = levelSprites[2];

            PlayerData playerData = new PlayerData();
            PlayerData savedData = SaveDataManager.LoadGameState();

            if (savedData != null)
            {
                levelTwoState = savedData.levelTwoState;
                levelThreeState = savedData.levelThreeState;
                levelFourState = savedData.levelFourState;
                levelFiveState = savedData.levelFiveState;
            }

            playerData.levelTwoState = levelTwoState;
            playerData.levelThreeState = levelThreeState;
            playerData.levelFourState = 2;
            playerData.levelFiveState = levelFiveState;

            SaveDataManager.SaveLevelData(playerData);
        }
        else if (levelFourState == 2)
        {
            levels[2].sprite = levelSprites[2];
            locks[2].GetComponent<Animator>().SetInteger("locked", 2);
        }
        else if (levelFourState == 0)
        {
            levels[2].sprite = darkSprites[2];
            locks[2].GetComponent<Animator>().SetInteger("locked", 0);
        }

        if (levelFiveState == 1)
        {
            locks[3].GetComponent<Animator>().SetInteger("locked", 1);
            levels[3].sprite = levelSprites[3];

            PlayerData playerData = new PlayerData();
            PlayerData savedData = SaveDataManager.LoadGameState();

            if (savedData != null)
            {
                levelTwoState = savedData.levelTwoState;
                levelThreeState = savedData.levelThreeState;
                levelFourState = savedData.levelFourState;
                levelFiveState = savedData.levelFiveState;
            }

            playerData.levelTwoState = levelTwoState;
            playerData.levelThreeState = levelThreeState;
            playerData.levelFourState = levelFourState;
            playerData.levelFiveState = 2;

            SaveDataManager.SaveLevelData(playerData);
        }
        else if (levelFiveState == 2)
        {
            levels[3].sprite = levelSprites[3];
            locks[3].GetComponent<Animator>().SetInteger("locked", 2);
        }
        else if (levelFiveState == 0)
        {
            levels[3].sprite = darkSprites[3];
            locks[3].GetComponent<Animator>().SetInteger("locked", 0);
        }
    }



    public void BackToCharacter()
    {
        SceneManager.LoadScene("ChooseCharacter");
        Time.timeScale = 1.0f;
    }

    public void Level1()
    {
        Health.gameEnded = false;
        LevelChossen = 1;
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1.0f;
        InputManager.DisableInGame();
    }
    public void Level2()
    {
        Health.gameEnded = false;
        if (levelTwoState == 1 || levelTwoState == 2)
        {
            LevelChossen = 2;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }
    public void Level3()
    {
        Health.gameEnded = false;
        if (levelThreeState == 1|| levelThreeState == 2)
        {
            LevelChossen = 3;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }
    public void Level4()
    {
        Health.gameEnded = false;
        if (levelFourState == 1 || levelFourState == 2)
        {
            LevelChossen = 4;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }

    public void Level5()
    {
        Health.gameEnded = false;
        if (levelFiveState == 1 || levelFiveState == 2) 
        {
            LevelChossen = 5;
            SceneManager.LoadScene("MainGame");
            Time.timeScale = 1.0f;
            InputManager.DisableInGame();

        }
    }

    public int Level() { return LevelChossen; }
}
