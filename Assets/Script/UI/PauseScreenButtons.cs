using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] Assets;
    [SerializeField] private UnityEngine.UI.Toggle[] toggles;
    [SerializeField] private GameObject[] Target;
    private UnityEngine.UI.Image image;
    [SerializeField] private AudioSource[] audioSource;
    Volume volume;
    [SerializeField] private Sprite[] offOn;

    private bool musicOn = true;
    private bool sfxOn = true;

    private int volumeAmount;

    private void Start()
    {
        LoadSound();
    }

    private void Update()
    {
        volume = FindObjectOfType<Volume>();
        MusicToggleCheck();
        SfxToggleCheck();
    }
    private void LoadSound()
    {
        PlayerSoundData playerData = PlayerSoundManager.LoadGameState();
        if (playerData != null)
        {
            volumeAmount = playerData.soundLevel;
            musicOn = playerData.musicOn;
            sfxOn = playerData.sfxOn;
        }
    }

    private void SaveSound()
    {
        PlayerSoundData playerData = new PlayerSoundData();
        playerData.soundLevel = volumeAmount;
        playerData.sfxOn = sfxOn;
        playerData.musicOn = musicOn;
        PlayerSoundManager.SaveLevelData(playerData);
    }

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        print(musicOn);
        SaveSound();
        print(musicOn);
        
    }

    public void ToggleSfx()
    {
        sfxOn = !sfxOn;
        SaveSound();
    }

    private void MusicToggleCheck()
    {
       
        image = Target[0].GetComponent<UnityEngine.UI.Image>();

        if (musicOn)
        {
            audioSource[0].enabled = true;
            audioSource[0].Play();

            image.sprite = offOn[1];
        }
        else
        {
            audioSource[0].enabled = false;
            image.sprite = offOn[0];
        }
    }

    private void SfxToggleCheck()
    {
        image = Target[1].GetComponent<UnityEngine.UI.Image>();

        if (sfxOn)
        {
            audioSource[1].enabled = true;
            audioSource[2].enabled = true;
            image.sprite = offOn[1];
        }
        else
        {
            image.sprite = offOn[0];
            audioSource[1].enabled = false;
            audioSource[2].enabled = false;
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
