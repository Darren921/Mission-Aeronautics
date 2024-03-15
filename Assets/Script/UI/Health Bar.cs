using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Sprite[] bars;
    [SerializeField] private Sprite[] enemyBars;

    [SerializeField] private Image playerHealthImage;
    [SerializeField] private Image enemyHealthImage;

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
        if (Tutorial.tutFin == true)
        {
            enemyHealthImage.sprite = enemyBars[levelPick.Level() - 1 ];
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
