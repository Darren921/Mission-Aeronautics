using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    private int volumeAmount;

    private bool musicOn;
    private bool sfxOn;

    public Image volumeImage;
    public Image musicImage;
    public Image sfxImage;

    public Sprite[] volumeSprites;
    public Sprite[] musicSprites;
    public Sprite[] sfxSprites;

    public AudioSource background;
    public AudioSource soundEffects;


    private void Start()
    {
        LoadSound();
    }
    

    private void Update()
    {
        if (volumeSprites != null && volumeSprites.Length > 0)
        {
            volumeImage.sprite = volumeSprites[volumeAmount];
        }

        background.volume = (float)volumeAmount / 4;
        soundEffects.volume = (float)volumeAmount / 4;

        MusicCheck();
        SfxCheck();
    }
    

    // Toggles
    public void MusicToggle()
    {
        musicOn = !musicOn;
        SaveSound();
    }

    public void SfxToggle()
    {
        sfxOn = !sfxOn;
        SaveSound();
    }


    // Buttons
    public void VolumeUp()
    {
        if (volumeAmount < 4)
        {
            volumeAmount += 1;
        }
        SaveSound();
    }

    public void VolumeDown()
    {
        if(volumeAmount > 0)
        {
            volumeAmount -= 1;
        }
        SaveSound();
    }






    // Check Functions
    private void MusicCheck()
    {
        if (musicOn)
        {
            background.enabled = true;
            if (musicSprites != null && musicSprites.Length > 0)
            {
                musicImage.sprite = musicSprites[0];
            }
        }
        else
        {
            background.enabled = false;
            if (musicSprites != null && musicSprites.Length > 0)
            {
                musicImage.sprite = musicSprites[1];
            }
        }
    }

    private void SfxCheck()
    {
        if (sfxOn)
        {
            soundEffects.enabled = true;
            if (musicSprites != null && musicSprites.Length > 0)
            {
                sfxImage.sprite = sfxSprites[0];
            }
            
        }
        else
        {
            soundEffects.enabled = false;
            if (musicSprites != null && musicSprites.Length > 0)
            {
                sfxImage.sprite = sfxSprites[1];
            }
        }
    }


    // functions
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
        playerData.musicOn = musicOn;
        playerData.sfxOn = sfxOn;
        PlayerSoundManager.SaveLevelData(playerData);
    }
}
