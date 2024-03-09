using System;
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
    [SerializeField] public Slider enemyHealthBarSlider;
    [SerializeField] private TextMeshProUGUI comboText;
    private  float enemyHealth;
    public int damage;
    [SerializeField] private Slider comboSlider;
    private int combo;

    public GameObject gameOverScreen;

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

    private PlayerData playerData = new PlayerData();
    

    void Start()
    {
        canAttack = true;
        enemyHealth = 100;
        player = GameObject.Find("Player").GetComponent<Player>();
        enemy = FindObjectOfType<BrickManAI>();
    }


    void Update()
    {
        comboText.text = combo.ToString();
        enemyHealthBarSlider.value = enemyHealth;
        print(enemyHealthBarSlider.value);
        comboSlider.value = combo;


        if (enemy.ReturnBulletHit() == true)
        {
            damage = 5;
            if (enemyHealth >= 0)
            {
                enemyHealth  -= damage;
            }
        }
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
        
        if (enemyHealth <= 0)
        {
            player.StopAllCoroutines();
            StopAllCoroutines();
            player.ReturnAnimator().SetBool("Idle", true);
            enemy.ReturnAnimator().SetBool("Stun", false);
            Time.timeScale = 0;
            //SceneManager.UnloadSceneAsync("MainGame");
            //SceneManager.LoadSceneAsync("ChooseCharacter");

            playerData.levelTwoActive = true;
            string json = JsonUtility.ToJson(playerData);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

            gameOverScreen.SetActive(true);
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

    
   
}
