using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider enemyHealthBarSlider;
    [SerializeField] private TextMeshProUGUI comboText;
    public float enemyHealth;
    public int damage;
    [SerializeField] private Slider comboSlider;
    private int combo;

    

    [SerializeField] private GameObject playerChar;
    [SerializeField] private GameObject enemyChar;

    public static bool enemyStunned;

    private bool canAttack;

    private static Player player;

    private static BrickManAI brickManAI;
    private static EarthmanAI earthmanAI;
    void Start()
    {
        canAttack = true;
        enemyHealth = 100;
        player = GameObject.Find("Player").GetComponent<Player>();
        brickManAI = new BrickManAI();
        earthmanAI = new EarthmanAI();
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
                        damage = 10;
                        if (enemyHealth >= 0)
                        {
                            enemyHealth -= damage;
                        }
                        
                        canAttack = false;
                    }
                    else
                    {
                        combo = 0;
                        damage = 30;
                        if (enemyHealth >= 0)
                        {
                            enemyHealth -= damage;
                        }
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

        if (brickManAI.Stun() == false || earthmanAI.Stun() == false)
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
