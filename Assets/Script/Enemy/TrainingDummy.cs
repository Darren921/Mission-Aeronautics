using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : Enemy
{
    Tutorial tut;



    // Start is called before the first frame update
    void Start()
    {
        tut = FindObjectOfType<Tutorial>();
        health = new Health();
        enemy = new Enemy();
        animator = GetComponent<Animator>();
        stunned = false;
        animator.SetBool("Stun", false);
        enemyState = "Idle";
        canAttack = false;
    }


    // Update is called once per frame
    void Update()
    {
        distance = (transform.position.x - player.transform.position.x);

        if (health.GetStunned())
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", true);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            stunDebounce += 1 * Time.deltaTime;
            if (stunDebounce >= 1)
            {
                stunned = false;
                if (tut.block == true)
                {
                    enemyState = "Recovery";
                    canAttack = true;

                }
                stunDebounce = 0;
                debounce = 0;
            }
        }
        else
        {
            stunned = true;
            if (enemyState == "Idle")
            {
                animator.SetBool("Move", false);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", false);

                debounce += 1 * Time.deltaTime;
                if (tut.block == true && tut.powerUps == false)
                {
                    if (debounce >= 1.5f)
                    {

                        enemyState = "Moving";
                        debounce = 0;

                    }
                }
            }
            else if (enemyState == "Moving")
            {
                animator.SetBool("Move", true);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", false);

                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);

                if (tut.block == true)
                {
                    if (distance <= 3 && distance >= -3)
                    {
                        int roll = Random.Range(0, 5);

                        if (roll == 0)
                        {
                            enemyState = "Attack 2";
                        }
                        else
                        {
                            enemyState = "Attack 1";
                        }
                    }
                }
            }
            else if (enemyState == "Attack 1")
            {
                animator.SetBool("Move", false);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", true);
                animator.SetBool("Attack 2", false);

                if (distance >= 1.5 && distance <= -1.5)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
                }

                Attack(40);

                if (tut.block == true)
                {
                    debounce += 1 * Time.deltaTime;
                    if (debounce >= 1)
                    {
                        enemyState = "Recovery";
                        debounce = 0;
                    }
                }

            }
            else if (enemyState == "Attack 2")
            {
                animator.SetBool("Move", false);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", true);

                Attack(40);

                if (tut.block == true)
                {
                    debounce += 1 * Time.deltaTime;
                    if (debounce >= 1)
                    {
                        enemyState = "Recovery";
                        debounce = 0;
                    }
                }

            }

            else if (enemyState == "Recovery")
            {
                if (tut.block == true)
                {

                    canAttack = true;
                    animator.SetBool("Move", true);
                    animator.SetBool("Stun", false);
                    animator.SetBool("Attack 1", false);
                    animator.SetBool("Attack 2", false);

                    if (distance <= 4.5 && distance >= -4.5)
                    {
                        if (distance >= 0)
                        {
                            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 5, this.transform.position.y), speed * Time.deltaTime);
                        }
                        else
                        {
                            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 5, this.transform.position.y), speed * Time.deltaTime);
                        }

                    }

                    debounce += 1 * Time.deltaTime;
                    if (debounce >= .8)
                    {
                        enemyState = "Moving";
                        debounce = 0;
                    }
                }
            }
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

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        bulletHit = false;
    }
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
    internal void Attack(float damage)
    {
        if (tut.block == true)
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



}


