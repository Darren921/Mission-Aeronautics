using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider enemyHealthBarSlider;
    [SerializeField] private TextMeshProUGUI comboText;
    public float playerHealth;
    public float enemyHealth;

    [SerializeField] private Slider comboSlider;
    private int combo;

    

    [SerializeField] private GameObject playerChar;
    [SerializeField] private GameObject enemyChar;

    public static bool enemyStunned;

    private bool canAttack;

    private static Player player;

    private static BrickManAI brickManAI;
    void Start()
    {
        canAttack = true;
        playerHealth = 100;
        enemyHealth = 100;
        player = GameObject.Find("Player").GetComponent<Player>();
        brickManAI = new BrickManAI();
    }


    void Update()
    {
        comboText.text = combo.ToString();

        comboSlider.value = combo;

        if (player.Performing())
        {
            if (player.Colliding())
            {
                if (canAttack)
                {
                    if (!player.SpecialAtk() == true)
                    {
                        if(combo <= 5)
                        {
                            combo += 1;
                        }
                        enemyStunned = true;
                        enemyHealth -= 10;
                        canAttack = false;
                    }
                    else
                    {
                        combo = 0;
                        enemyHealth -= 30;
                        canAttack = false;
                    }
                  
                }
            }
        }
        else
        {
            canAttack = true;
        }
            
        enemyHealthBarSlider.value = enemyHealth;

        if (brickManAI.Stun() == false)
        {
            enemyStunned = false;
        }
    }
    public int GetCombo()
    {
        return combo;
    }

    public bool GetStunned()
    {
        return enemyStunned;
    }
}
