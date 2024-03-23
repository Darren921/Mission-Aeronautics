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

    public AudioSource volumeThing;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 0.75f);
            PlayerPrefs.Save();
        }
        volumeThing.volume = PlayerPrefs.GetFloat("Volume", 0.75f);
        PlayerPrefs.Save();

        print(volumeThing.volume);

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("Volume", volumeThing.volume);
        PlayerPrefs.Save();
        print(currentVolume);
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
            volumeThing.volume = (float)volumeAmount / 4;
            PlayerPrefs.SetFloat("Volume", volumeThing.volume);
            print(PlayerPrefs.GetFloat("Volume"));
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
