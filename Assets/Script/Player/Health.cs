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
    public static bool gameEnded;
    private float damageResistance;


    [SerializeField] private Sprite[] enemyBars;
    [SerializeField] private Image enemyHealthImage;


    [SerializeField] public Slider enemyHealthBarSlider;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private float enemyHealth;
    public int damage;
    private Tutorial tut;
    private TutTextManager tutTextManager;
    [SerializeField] private Slider comboSlider;
    private int combo;

    public GameObject gameOverScreen;
    public TextMeshProUGUI winText;

    public Sprite[] winImages;
    public Image winTextImage;

    public static bool enemyStunned;
    public static bool enemyStunnedFire;

    private float debounce = 0;

    private bool tempTestThing;

    public float GetEnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value;}
    }
    public int GetCombo
    {
       get { return combo; }
       set { combo = value; }
    }

    public bool GetStunned
    {
        get { return enemyStunned; }
        set { enemyStunned = value; }
    }

    public bool GetStunnedFire
    {
        get { return enemyStunnedFire; }
        set { enemyStunnedFire = value; }
    }

    [SerializeField] private GameObject playerChar;
    [SerializeField] private GameObject enemyChar;

    private bool canAttack;

    private static Player player;

    private static Enemy enemy;

    private PlayerData playerData = new PlayerData();
    private LevelPick levelPick = new LevelPick();
    
    public  bool nextSetennce;

    public GameObject playerHealth;

    void Start()
    {

        tut = FindObjectOfType<Tutorial>();
        tutTextManager = FindObjectOfType<TutTextManager>();
        canAttack = true;
        enemyHealth = 100;
        player = GameObject.Find("Player").GetComponent<Player>();
        if (Tutorial.tutFin == true)
        {
            switch (LevelPick.LevelChossen)
            {
               
                case 1:
                    enemy = FindObjectOfType<BrickManAI>();
                    enemyHealthImage.sprite = enemyBars[0];
                    damage = 8;
                    break;
                case 2:
                    enemy = FindObjectOfType<EvilDarrenAI>();
                    enemyHealthImage.sprite = enemyBars[1];
                    damage = 8;
                    break;
                case 3:
                    enemy = FindObjectOfType<EarthmanAI>();
                    enemyHealthImage.sprite = enemyBars[2];
                    damage = 8;
                    break;
                case 4:
                    enemy = FindObjectOfType<BigBirdAI>();
                    enemyHealthImage.sprite = enemyBars[3];
                    damage = 8;
                    break;
                case 5:
                    enemy = FindObjectOfType<XerosAI>();
                    enemyHealthImage.sprite = enemyBars[4];
                    damage = 5;
                    break;
            }

        }
        else
        {
            enemy = FindObjectOfType<TrainingDummy>();
            damage = 16;
        }
     
    }


    void Update()
    {
        if (enemyStunnedFire == tempTestThing)
        {

        }
        else
        {
            print("ENEMY STUN THING: " + enemyStunnedFire);
            tempTestThing = enemyStunnedFire;
        }
        

        if (enemyStunned)
        {
            debounce += 1 * Time.deltaTime;

            if (debounce >= 3)
            {
                debounce = 0;
                enemyStunnedFire = false;
            }
        }


        //print(enemyHealth);
        if(Tutorial.tutFin != true)
        {
            enemy = FindObjectOfType<TrainingDummy>();
        }
        comboText.text = "x" + combo.ToString();
        enemyHealthBarSlider.value = enemyHealth;
        comboSlider.value = combo;

        if(enemy != null && Tutorial.tutFin == true)
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
        if(enemy != null && Tutorial.tutFin == false)
        {
            if (enemy.ReturnBulletHit() == true)
            {
                damage = 100;
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
                            enemyStunnedFire = true;
                            
                            //damage = 10;
                            if (enemyHealth >= 0)
                            {
                                enemyHealth -= damage;
                            }

                            canAttack = false;
                        }

                        else
                        {
                            damage = damage * 5;
                            if (enemyHealth >= 0)
                            {
                                enemyHealth -= damage;
                            }
                            canAttack = false;
                            damage = damage / 5;
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
            gameEnded = true;

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
            //winText.text = "You Win!";
            winTextImage.sprite = winImages[0];
        }
        if (enemy != null) {
        if (enemy.GetPlayerHealth <= 0 && Tutorial.tutFin == true)
            {
                gameEnded = true;

                InputManager.DisableInGame();
                player.ReturnAnimator().SetBool("IsMoving", false);
                player.ReturnAnimator().SetBool("Stunned", false);
                player.ReturnAnimator().SetBool("IsAttacking", false);
                player.GetStunned = false;
                player.StopAllCoroutines();
                StopAllCoroutines();
                Time.timeScale = 0;

                gameOverScreen.SetActive(true);
                //winText.text = "You Lose!";
                winTextImage.sprite = winImages[1];
            }
        }

        if (Tutorial.tutFin == false)
        {
            if (enemyHealth <= 0)
            {

                tutTextManager.IsTalking = false;
                tut.battleComplete = true;
            }



        }
        if (enemy != null)
        {
            if (enemy.Stun() == false)
            {
                enemyStunned = false;
            }
        }
        
    }
    
    public float returnEnemyHealth()
    {
        return enemyHealth;
    }
    

    
   
}
