using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Levels()
    {
        SceneManager.LoadScene("Levels");
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1.0f;
    }

    public void Characters()
    {
        SceneManager.LoadScene("ChooseCharacter");
        Time.timeScale = 1.0f;
    }
}
