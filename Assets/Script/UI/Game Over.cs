using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Levels()
    {
        SceneManager.UnloadSceneAsync("MainGame");
        SceneManager.LoadSceneAsync("Levels");
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.UnloadSceneAsync("MainGame");
        SceneManager.LoadSceneAsync("MainGame");
        Time.timeScale = 1.0f;
    }

    public void Characters()
    {
        SceneManager.UnloadSceneAsync("MainGame");
        SceneManager.LoadSceneAsync("ChooseCharacter");
        Time.timeScale = 1.0f;
    }
}
