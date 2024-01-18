using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void loadSinglePlayer()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
    public void loadMultiPlayer()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
