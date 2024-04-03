using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    private int volumeAmount;
    public Image volumeImage;

    public Sprite[] volumeSprites;

    public AudioSource background;
    public AudioSource soundEffects;


    private void Start()
    {
        //if (!PlayerPrefs.HasKey("VolumeBackground") && !PlayerPrefs.HasKey("VolumeEffects"))
        //{
        //    PlayerPrefs.SetFloat("VolumeBackground", 0.75f);
        //    PlayerPrefs.SetFloat("VolumeEffects", 0.75f);
        //    PlayerPrefs.Save();
        //}
        //background.volume = PlayerPrefs.GetFloat("VolumeBackground", 0.75f);
     
        //soundEffects.volume = PlayerPrefs.GetFloat("VolumeEffects", 0.75f);
        //PlayerPrefs.Save();

        //print(soundEffects.volume);
        //print(background.volume);

        LoadSound();
    }

    private void LoadSound()
    {
        PlayerSoundData playerData = PlayerSoundManager.LoadGameState();
        if (playerData != null)
        {
            volumeAmount = playerData.soundLevel;
        }
    }

    private void OnDestroy()
    {
        //PlayerPrefs.SetFloat("VolumeBackground", background.volume);
        //PlayerPrefs.SetFloat("VolumeEffects", soundEffects.volume); game say's its been destroyed and gave an error so i temporarily commented it
        

        //PlayerPrefs.Save();
        //print(background.volume);
        //print(soundEffects.volume);
    }
    private void Update()
    {
        if (volumeSprites != null && volumeSprites.Length > 0)
        {
            volumeImage.sprite = volumeSprites[volumeAmount];
            //PlayerPrefs.SetFloat("VolumeBackground", background.volume);
            //PlayerPrefs.SetFloat("VolumeEffects", soundEffects.volume);
            //print(PlayerPrefs.GetFloat("VolumeBackground"));
            //print(PlayerPrefs.GetFloat("VolumeEffects"));
            //PlayerPrefs.Save();
        }

        background.volume = (float)volumeAmount / 4;
        soundEffects.volume = (float)volumeAmount / 4;
    }

    public void VolumeUp()
    {
        if (volumeAmount < 4)
        {
            volumeAmount += 1;
        }

        PlayerSoundData playerData = new PlayerSoundData();
        playerData.soundLevel = volumeAmount;
        PlayerSoundManager.SaveLevelData(playerData);

        print(playerData.soundLevel);
    }

    public void VolumeDown()
    {
        if(volumeAmount > 0)
        {
            volumeAmount -= 1;
        }

        PlayerSoundData playerData = new PlayerSoundData();
        playerData.soundLevel = volumeAmount;
        PlayerSoundManager.SaveLevelData(playerData);

        print(playerData.soundLevel);
    }
}
