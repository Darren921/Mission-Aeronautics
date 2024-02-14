using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public static int CharacterChossen;


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
    }

    public void ChoosenCharacter1()
    {
        CharacterChossen = 1;
        SceneManager.LoadScene("Levels");
    }
    public void ChoosenCharacter2()
    {
        CharacterChossen = 2;
        SceneManager.LoadScene("Levels");
    }
    public void ChoosenCharacter3()
    {
        CharacterChossen = 3;
        SceneManager.LoadScene("Levels");
    }
    public void ChoosenCharacter4()
    {
        CharacterChossen = 4;
        SceneManager.LoadScene("Levels");
    }
   
    /*
public void loadMultiPlayer()
{
SceneManager.LoadScene("ChooseCharacter");
}
*/
}
