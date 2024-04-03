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
    Volume volume;
    [SerializeField] private Sprite[] offOn;
    private void Update()
    {
        volume = FindObjectOfType<Volume>();
    }
    public void MusicToggle()
    {
       
        var Switch = toggles[0];
        image = Target[0].GetComponent<UnityEngine.UI.Image>();
       
        if (Switch.isOn == false)
        {
            audioSource[0].enabled = false;
            image.sprite = offOn[0];
            PlayerPrefs.SetFloat("VolumeBackground", volume.background.volume);
            PlayerPrefs.Save();
        }
        else
        {
            audioSource[0].enabled = true;
            audioSource[0].Play();
            PlayerPrefs.GetFloat("VolumeBackground", 0.75f);
            PlayerPrefs.Save();

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
            PlayerPrefs.SetFloat("VolumeBackground", volume.background.volume);
            PlayerPrefs.SetFloat("VolumeEffects", volume.soundEffects.volume);
            PlayerPrefs.Save();


        }
        else
        {
            audioSource[1].enabled = true;
            audioSource[2].enabled = true;
            image.sprite = offOn[1];
            PlayerPrefs.GetFloat("VolumeBackground", 0.75f );
            PlayerPrefs.GetFloat("VolumeEffects", 0.75f);
            PlayerPrefs.Save();
        }


    }
    public void OpenPauseMenu()
    {
        for (int i = 0; i < Assets.Length - 2; i++)
        {
            Assets[i].SetActive(true);
            Time.timeScale = 0;
        }
        Assets[11].SetActive(true);
    }

    public void OpenControls()
    {
        ClosePauseMenu();
        Time.timeScale = 0;
        Assets[9].SetActive(false);
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
        for (int i = 0; i < Assets.Length - 2; i++)
        {
            Assets[i].SetActive(false);
            Time.timeScale = 1;
        }
        Assets[11].SetActive(false);
    }
    

}
