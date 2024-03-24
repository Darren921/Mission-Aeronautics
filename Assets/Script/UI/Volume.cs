using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public int volumeAmount = 4;
    public float currentVolume;
    public Image volumeImage;

    public Sprite[] volumeSprites;

    public AudioSource background;
    public AudioSource soundEffects;



    private void Start()
    {
        if (!PlayerPrefs.HasKey("VolumeBackground") && !PlayerPrefs.HasKey("VolumeEffects"))
        {
            PlayerPrefs.SetFloat("VolumeBackground", 0.75f);
            PlayerPrefs.SetFloat("VolumeEffects", 0.75f);
            PlayerPrefs.Save();
        }
        background.volume = PlayerPrefs.GetFloat("VolumeBackground", 0.75f);
     
        soundEffects.volume = PlayerPrefs.GetFloat("VolumeEffects", 0.75f);
        PlayerPrefs.Save();

        print(soundEffects.volume);
           

        

        print(background.volume);

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("VolumeBackground", background.volume);
        //PlayerPrefs.SetFloat("VolumeEffects", soundEffects.volume); game say's its been destroyed and gave an error so i temporarily commented it
        

        PlayerPrefs.Save();
        print(background.volume);
        //print(soundEffects.volume);
    }
    private void Update()
    {
        if (volumeSprites != null && volumeSprites.Length > 0)
        {
            UpdateVol();
        }
      
    }

    public void UpdateVol()
    {
        if(volumeSprites != null && volumeSprites.Length > 0 ) 
        {
            volumeImage.sprite = volumeSprites[volumeAmount];
            background.volume = (float)volumeAmount / 4;
            soundEffects.volume = (float)volumeAmount / 4;
            PlayerPrefs.SetFloat("VolumeBackground", background.volume);
            PlayerPrefs.SetFloat("VolumeEffects", soundEffects.volume);
            print(PlayerPrefs.GetFloat("VolumeBackground"));
            print(PlayerPrefs.GetFloat("VolumeEffects"));
            PlayerPrefs.Save();


        }



    }

    public void VolumeUp()
    {
        if (volumeAmount < 4)
        {
            volumeAmount += 1;
        }
    }

    public void VolumeDown()
    {
        if(volumeAmount > 0)
        {
            volumeAmount -= 1;
        }
    }
}
