using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public int volumeAmount = 4;

    public Image volumeImage;

    public Sprite[] volumeSprites;

    public AudioSource volumeThing;

    private void Update()
    {
        volumeImage.sprite = volumeSprites[volumeAmount];
        volumeThing.volume = (float)volumeAmount / 4;
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
