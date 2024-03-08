using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider enemyHealthBarSlider;
    [SerializeField] private TextMeshProUGUI comboText;
    private float enemyHealth;
    public int damage;
    [SerializeField] private Slider comboSlider;
    private int combo;

    public float GetEnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value;}
    }
    public  int GetCombo
    {
       get { return combo; }
       set { combo = value; }

    }

    [SerializeField] private GameObject playerChar;
    [SerializeField] private GameObject enemyChar;

    public static bool enemyStunned;

    private bool canAttack;

    private static Player player;

    private static Enemy enemy;
    void Start()
    {
        canAttack = true;
        enemyHealth = 100;
        player = GameObject.Find("Player").GetComponent<Player>();
        enemy = new Enemy();
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
            
        enemyHealthBarSlider.value = GetEnemyHealth;

        if (enemyHealth <= 0)
        {
            Time.timeScale = 0;
            SceneManager.UnloadSceneAsync("MainGame");
            SceneManager.LoadSceneAsync("ChooseCharacter");
            StopAllCoroutines();
        }

        if (enemy.Stun() == false)
        {
            enemyStunned = false;
        }
    }
   

    public bool GetStunned()
    {
        return enemyStunned;
    }
    public Slider returnEnemyHealthBarSlider()
    {
        return enemyHealthBarSlider;
    }
}
