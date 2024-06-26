using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrickManAI : Enemy
{
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    private Vector3 teleportLocation;

    private bool canMoveUp = false;

    private int punchCount = 0;

    void Start()
    {
        health = new Health();
        enemy = FindObjectOfType<Enemy>();
        animator = GetComponent<Animator>();
        enemyState = "Idle";
        canAttack = true;
    }
   
    void Update()
    {
        distance = (transform.position.x - player.transform.position.x);

        if (enemyState == "Idle")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            debounce += 1 * Time.deltaTime;

            if (debounce >= .5f)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
        else if (enemyState == "Move")
        {
            canAttack = true;
            animator.SetBool("Move", true);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);


            if (distance <= 3 && distance >= -3)
            {
                enemyState = "Prepare Attack";
                debounce = 0;
            }
        }
        else if (enemyState == "Prepare Attack")
        {
            if (health.GetStunnedFire)
            {
                debounce = 0;
                enemyState = "Stunned";
            }

            debounce += 1 * Time.deltaTime;

            if (debounce >= 0.2)
            {
                source.PlayOneShot(AttackEffects[0]);
                enemyState = "Punch";
                debounce = 0;
            }
        }
        else if (enemyState == "Punch")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            if (distance >= 1.5 && distance <= -1.5)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }

            BrickAttack(5, false);

            debounce += 1 * Time.deltaTime;
            if (debounce >= .5)
            {
                enemyState = "After Punch";
                debounce = 0;
            }
        }
        else if (enemyState == "After Punch")
        {
            debounce += 1 * Time.deltaTime;

            if (debounce >= 0.3)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
        else if (enemyState == "Stunned")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", true);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            stunDebounce += 1 * Time.deltaTime;
            if (stunDebounce >= 1.5)
            {
                health.GetStunnedFire = false;
                enemyState = "Recover";
                canAttack = true;
                stunDebounce = 0;
                debounce = 0;

                int e = Random.Range(0, 3);

                if (e == 2)
                {
                    canMoveUp = true;
                }
            }
        }
        else if (enemyState == "Recover")
        {
            animator.SetBool("Move", true);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            if (enemy.GetTurn1())
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 13, this.transform.position.y), speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 13, this.transform.position.y), speed * Time.deltaTime);
            }

            if (canMoveUp)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y + 10), speed * Time.deltaTime);
            }

            if (distance > 8 || distance < -8)
            {
                canMoveUp = false;

                punchCount++;
                if (punchCount >= 2)
                {
                    enemyState = "Pre Teleport";
                    punchCount = 0;
                }
                else
                {
                    enemyState = "Post Recovery";
                    debounce = 0;
                }
            }

            if (enemyState == "Recover")
            {
                debounce += Time.deltaTime;

                if (debounce >= 4)
                {
                    punchCount++;
                    if (punchCount >= 2)
                    {
                        enemyState = "Pre Teleport";
                        punchCount = 0;
                    }
                    else
                    {
                        enemyState = "Post Recovery";
                        debounce = 0;
                    }
                    debounce = 0;
                }
            }
        }
        else if (enemyState == "Post Recovery")
        {
            debounce += 1 * Time.deltaTime;

            if (debounce > .5)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
        else if (enemyState == "Pre Teleport")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            if (enemy.GetTurn1())
            {
                teleportLocation = new Vector3(player.transform.position.x - 2, player.transform.position.y, this.transform.position.z);
            }
            else
            {
                teleportLocation = new Vector3(player.transform.position.x + 2, player.transform.position.y, this.transform.position.z);
            }

            enemyState = "Prepare Teleport";
            debounce = 0;
        }
        else if (enemyState == "Prepare Teleport")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", true);

            debounce += 1 * Time.deltaTime;

            if (debounce >= 2.5)
            {
                enemyState = "Teleport";
                debounce = 0;
            }
        }
        else if (enemyState == "Teleport")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            source.PlayOneShot(AttackEffects[1]);

            this.transform.position = teleportLocation;

            enemyState = "After Teleport";
            debounce = 0;
        }
        else if (enemyState == "After Teleport")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            if (distance >= 1.5 || distance <= -1.5)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }

            debounce += 1 * Time.deltaTime;

            if (debounce > 0.5)
            {
                debounce = 0;
                enemyState = "Finish Teleport";
                source.PlayOneShot(AttackEffects[0]);
            }
            
        }
        else if (enemyState == "Finish Teleport")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);
            animator.SetBool("Recover", false);
            animator.SetBool("Teleport", false);

            BrickAttack(15, false);

            debounce += 1 * Time.deltaTime;
            if (debounce >= 1)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
    }

    internal void BrickAttack(float damage, bool isProj)
    {
        if (collidingWithPlayer)
        {
            print(collidingWithPlayer);
            if (canAttack)
            {
                if (player.GetComponent<Player>().returnisBlocking() != true)
                {
                    print("Return Fired");
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
                if (player.GetComponent<Player>().returnShieldPowerActive() == true)
                {

                    playerHealth -= (damage * 0);
                    playerSlider.value = playerHealth;
                    canAttack = false;
                    playerHit = false;
                }
            }
        }
        else if (isProj)
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
            if (player.GetComponent<Player>().returnShieldPowerActive() == true)
            {

                playerHealth -= (damage * 0);
                playerSlider.value = playerHealth;
                canAttack = false;
                playerHit = false;
            }
            canAttack = true;
        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //   if (collision.gameObject.tag == "Player")
    // {
    //   collidingWithPlayer = true;
    //}
    //if (collision.gameObject.tag == ("PlayerProjectiles"))
    //{
    //   if (bulletHit == true)
    //     return;
    //bulletHit = true;
    //StartCoroutine(Reset());

    //}

    //}


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //  {
    //    collidingWithPlayer = false;
    //}
    //}

    internal Animator ReturnAnimator()
    {
        return animator;
    }
    /*internal void Attack(float damage)
    {
        if (Tutorial.tutFin == true)
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
       
        
    }*/

    internal float GetPlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }



}
