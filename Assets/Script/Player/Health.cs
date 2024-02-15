using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    [SerializeField] private Slider playerHealthBarSlider;
    [SerializeField] private Slider enemyHealthBarSlider;
    public float playerHealth;
    public float enemyHealth;

    [SerializeField] private TextMeshProUGUI comboCounter;
    private int combo;

    

    [SerializeField] private GameObject playerChar;
    [SerializeField] private GameObject enemyChar;

    private bool canAttack;

    private static Player player;
    void Start()
    {
        canAttack = true;
        playerHealth = 100;
        enemyHealth = 100;
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update()
    {
        comboCounter.text = "Combo: " + combo.ToString();

        if (player.Performing())
        {
            if (player.Colliding())
            {
                if (canAttack)
                {
                    if (!player.SpecialAtk() == true)
                    {
                        combo += 1;
                        enemyHealth -= 10;
                        canAttack = false;
                    }
                    else
                    {
                        combo = 0;
                        enemyHealth -= 25;
                        canAttack = false;
                    }
                  
                }
            }
        }
        else
        {
            canAttack = true;
        }
            
        playerHealthBarSlider.value = playerHealth;
        enemyHealthBarSlider.value = enemyHealth;
    }
    public int GetCombo()
    {
        return combo;
    }

}
