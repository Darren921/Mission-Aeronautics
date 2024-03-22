using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] Assets;
    // Start is called before the first frame update
    [SerializeField] private UnityEngine.UI.Toggle[] toggles;
    [SerializeField] private GameObject[] Target;
    private UnityEngine.UI.Image image;
    [SerializeField] private AudioSource[] audioSource;

    [SerializeField] private Sprite[] offOn;
    public void MusicToggle()
    {
       
        var Switch = toggles[0];
        image = Target[0].GetComponent<UnityEngine.UI.Image>();
       
        if (Switch.isOn == false)
        {
            audioSource[0].enabled = false;
            image.sprite = offOn[0];
        }
        else
        {
            audioSource[0].enabled = true;
            audioSource[0].Play();
            image.sprite = offOn[1];
        }
    }
    public void SFXToggle()
    {
        var Switch = toggles[1];
        image = Target[1].GetComponent<UnityEngine.UI.Image>();

        if (Switch.isOn == false)
        {
            image.sprite = offOn[0];
            audioSource[1].enabled = false;
            audioSource[2].enabled = false;

        }
        else
        {
            audioSource[1].enabled = true;
            audioSource[2].enabled = true;
            image.sprite = offOn[1];
        }


    }
    public void OpenPauseMenu()
    {
        for (int i = 0; i < Assets.Length - 1; i++)
        {
            Assets[i].SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OpenControls()
    {
        ClosePauseMenu();
        Time.timeScale = 0;
        Assets[9].SetActive(true);
        Assets[10].SetActive(true);
    }

    public void CloseControls()
    {
        OpenPauseMenu();
        Time.timeScale = 0;
        Assets[9].SetActive(true);
        Assets[10].SetActive(false);
    }
    public void ClosePauseMenu()
    {
        for (int i = 0; i < Assets.Length - 1; i++)
        {
            Assets[i].SetActive(false);
            Time.timeScale = 1;
        }
    }
    

}
