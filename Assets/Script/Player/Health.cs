using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public Slider enemyHealthBarSlider;
    [SerializeField] private TextMeshProUGUI comboText;
    private  float enemyHealth;
    public int damage;
    private Tutorial tut;
    private TutTextManager tutTextManager;
    [SerializeField] private Slider comboSlider;
    private int combo;

    public GameObject gameOverScreen;
    public TextMeshProUGUI winText;

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

    private Enemy enemyScript = new Enemy();

    private PlayerData playerData = new PlayerData();
    private LevelPick levelPick = new LevelPick();
    
    public  bool nextSetennce;

    void Start()
    {
        tut = FindObjectOfType<Tutorial>();
        tutTextManager = FindObjectOfType<TutTextManager>();
        canAttack = true;
        enemyHealth = 100;
        player = GameObject.Find("Player").GetComponent<Player>();
        if (Tutorial.tutFin == false)
        {
            switch (LevelPick.LevelChossen)
            {
                case 0:
                    enemy = FindObjectOfType<TrainingDummy>();
                    break;
                case 1:
                    enemy = FindObjectOfType<BrickManAI>();
                    break;
                case 2:
                    enemy = FindObjectOfType<EarthmanAI>();
                    break;

            }

        }
     
    }


    void Update()
    {
        //print(enemyScript.GetPlayerHealth());
        if(Tutorial.tutFin != true)
        {
            enemy = FindObjectOfType<TrainingDummy>();
        }
        comboText.text = combo.ToString();
        enemyHealthBarSlider.value = enemyHealth;
        comboSlider.value = combo;

        if(enemy != null)
        {
            if (enemy.ReturnBulletHit() == true)
            {
                damage = 5;
                if (enemyHealth >= 0)
                {
                    enemyHealth -= damage;
                }
            }
        }
        if (enemy != null)
        {
            if (player.Performing())
            {
                if (player.Colliding())
                {
                    if (canAttack)
                    {
                        if (!player.SpecialAtk() == true)
                        {
                            if (combo <= 5)
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
        }
        
        if (enemyHealth <= 0 && Tutorial.tutFin == true)
        {
            
            InputManager.DisableInGame();
            player.ReturnAnimator().SetBool("IsMoving",false);
            player.ReturnAnimator().SetBool("Stunned", false);
            player.ReturnAnimator().SetBool("IsAttacking", false);
            player.GetStunned = false;
            player.StopAllCoroutines();
            StopAllCoroutines();
            Time.timeScale = 0;
            


            //SceneManager.UnloadSceneAsync("MainGame");
            //SceneManager.LoadSceneAsync("ChooseCharacter");

            if (levelPick.Level() == 1)
            {
                playerData.levelTwoActive = true;
            }
            else if (levelPick.Level() == 2)
            {
                playerData.levelThreeActive = true;
            }
            else if(levelPick.Level() == 3)
            {
                playerData.levelFourActive = true;
            }
            else if (levelPick.Level()== 4)
            {
                playerData.levelFiveActive = true;
            }
            
            string json = JsonUtility.ToJson(playerData);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

            gameOverScreen.SetActive(true);
            winText.text = "You Win!";
        }
        else if (enemyScript.GetPlayerHealth() <= 0  && Tutorial.tutFin == true)
        {
            InputManager.DisableInGame();
            player.ReturnAnimator().SetBool("IsMoving", false);
            player.ReturnAnimator().SetBool("Stunned", false);
            player.ReturnAnimator().SetBool("IsAttacking", false);
            player.GetStunned = false;
            player.StopAllCoroutines();
            StopAllCoroutines();
            Time.timeScale = 0;

            gameOverScreen.SetActive(true);
            winText.text = "You Lose!";
        }


        if(enemyHealth <= 0 && tut.block != true)
        {  
                
                tut.block = true;
                tut.CheckIfTrue();
            tutTextManager.IsTalking = false;


        }
       
        if (player.ReturnAnimator().GetBool("Guarding") == true && tut.block == true && enemy.ReturnplayerHit() == true)
        {
            
            tut.powerUps = true;
            tut.CheckIfTrue();

            tutTextManager.IsTalking = false;

        }
        if (enemy != null)
        {
            if (enemy.Stun() == false)
            {
                enemyStunned = false;
            }
        }
        
    }
   

    public bool GetStunned()
    {
        return enemyStunned;
    }

    
   
}
