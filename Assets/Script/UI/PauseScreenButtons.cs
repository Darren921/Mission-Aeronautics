using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] Assets;
    // Start is called before the first frame update
    

    public void OpenPauseMenu()
    {
        for (int i = 0; i < Assets.Length; i++)
        {
            Assets[i].SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ClosePauseMenu()
    {
        for (int i = 0; i < Assets.Length; i++)
        {
            Assets[i].SetActive(false);
            Time.timeScale = 1;
        }
    }
    

}
