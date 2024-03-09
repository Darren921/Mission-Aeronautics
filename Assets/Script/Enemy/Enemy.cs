using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;

    [SerializeField] private bool enemyActive;
    [SerializeField] private bool turnActive;

    [SerializeField] protected GameObject player;


    private LevelPick levelPick;
    private Rigidbody2D rb;

    private float time = 0f;

    protected Animator animator;
    private int level;

    private bool db1;
    private bool db2;

    [SerializeField] protected float speed;

    [SerializeField] public Slider playerSlider;

    [SerializeField] public float playerHealth = 100;


    protected float distance;

    public string enemyState = "Idle";

    protected bool collidingWithPlayer;

    protected bool playerHit;

    protected bool canAttack;

    protected static bool stunned;

    protected static Health health;
    protected static Enemy enemy;

    protected float debounce = 0f;
    protected float stunDebounce = 0f;
    protected bool bulletHit;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        levelPick = new LevelPick();

        level = levelPick.Level();


        animator.runtimeAnimatorController = animatorOverrideControllers[level - 1];

        db1 = true;
        db2 = false;
        
    }


    void Update()
    {
       // float distance = transform.position.x - player.transform.position.x;
        //print(transform.position.x - player.transform.position.x);

       // if ((distance <= 3 && distance >= -3))
       // {
       //     print(distance);
     //   }

        //switch case maybe is preffered here
        if (enemyActive)
        {
            if (level == 1)
            {
                this.gameObject.GetComponent<BrickManAI>().enabled = true;
            }
            else if (level == 2)
            {
                this.gameObject.GetComponent<EarthmanAI>().enabled = true;
            }
            else if (level == 3)
            {

            }
            else
            {

            }
        }
        else
        {
            this.gameObject.GetComponent<BrickManAI>().enabled = false;
            this.gameObject.GetComponent<EvilDarrenAI>().enabled = false;
            this.gameObject.GetComponent<EarthmanAI>().enabled = false;
        }

        if (turnActive)
        {
            if (transform.position.x < player.transform.position.x)
            {
                if (db1)
                {
                    time += 1 * Time.deltaTime;
                    if (time >= .5)
                    {
                        time = 0;
                        db1 = false;
                        db2 = true;
                        transform.Rotate(0, 180, 0);
                        player.transform.Rotate(0, -180, 0);
                    }
                }

            }
            else
            {
                if (db2)
                {
                    time += 1 * Time.deltaTime;
                    if (time >= .5)
                    {
                        time = 0;
                        db1 = true;
                        db2 = false;
                        transform.Rotate(0, -180, 0);
                        player.transform.Rotate(0, -180, 0);
                    }

                }
            }
        }

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = true;
        }
        if (collision.gameObject.tag == ("PlayerProjectiles"))
        {
            bulletHit = true;
          
        }
    }
  

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }


    public bool Stun()
    {
        return stunned;
    }

    public bool ReturnplayerHit()
    {
        return playerHit;
    }
    public bool ReturnBulletHit()
    {
        return bulletHit;
    }
    public bool GetTurn1()
    {
        return db1;
    }
    public bool GetTurn2()
    {
        return db2;
    }

    internal void Attack(float damage)
    {
        if (collidingWithPlayer)
        {
            if (canAttack)
            {
                if (player.GetComponent<Player>().returnisBlocking() != true)
                {
                    playerHealth -= damage;
                    playerSlider.value = playerHealth;
                    canAttack = false;
                    playerHit = true;
                }
                else
                {
                    playerHealth -= (damage * 0.3f);
                    playerSlider.value = playerHealth;
                    canAttack = false;
                    playerHit = true;
                }

            }
        }
    }

}
