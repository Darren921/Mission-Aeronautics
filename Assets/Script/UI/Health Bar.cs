using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Sprite[] bars;
    

    [SerializeField] private Image playerHealthImage;
    

    private LevelPick levelPick = new LevelPick();
    void Start()
    {
       
            switch (Buttons.CharacterChossen)
            {
                case 1:
                    playerHealthImage.sprite = bars[0];
                    break;
                case 2:
                    playerHealthImage.sprite = bars[1];
                    break;
                case 3:
                    playerHealthImage.sprite = bars[2];
                    break;
                case 4:
                    playerHealthImage.sprite = bars[3];
                    break;
            }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
