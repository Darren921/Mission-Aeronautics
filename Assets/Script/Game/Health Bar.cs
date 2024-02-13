using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Sprite[] bars;

    private Image health;
    void Start()
    {
        health = GetComponent<Image>();
        switch (Buttons.CharacterChossen)
        {
            case 1:
                health.sprite = bars[0];
                break;
            case 2:
                health.sprite = bars[1];
                break;
            case 3:
                health.sprite = bars[2];
                break;
            case 4:
                health.sprite = bars[3];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
