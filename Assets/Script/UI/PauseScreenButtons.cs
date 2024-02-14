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
    private AudioSource audioSource;

    [SerializeField] private Sprite[] offOn;
    public void MusicToggle()
    {
        audioSource = this.GetComponent<AudioSource>();
        var Switch = toggles[0];
        image = Target[0].GetComponent<UnityEngine.UI.Image>();
       
        if (Switch.isOn == false)
        {
            audioSource.enabled = false;
            image.sprite = offOn[0];
        }
        else
        {
            audioSource.enabled = true;
            audioSource.Play();
            image.sprite = offOn[1];
        }
    }
    public void SFXToggle()
    {
        audioSource = this.GetComponent<AudioSource>();
        var Switch = toggles[1];
        image = Target[1].GetComponent<UnityEngine.UI.Image>();

        if (Switch.isOn == false)
        {
            image.sprite = offOn[0];
        }
        else
        {

            image.sprite = offOn[1];
        }


    }
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
