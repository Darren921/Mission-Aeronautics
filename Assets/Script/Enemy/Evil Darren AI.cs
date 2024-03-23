using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvilDarrenAI : Enemy
{
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject shootThing;

    private bool canShoot = true;


    // Start is called before the first frame update
    void Start()
    {
        health = new Health();
        enemy = new Enemy();
        animator = GetComponent<Animator>();
        stunned = false;
        animator.SetBool("Stun", false);
        enemyState = "Moving";
        canAttack = true;
    }


    // Update is called once per frame
    void Update()
    {
        distance = (transform.position.x - player.transform.position.x);

        if (health.GetStunned)
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", true);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            stunDebounce += 1 * Time.deltaTime;
            if (stunDebounce >= 1)
            {
                stunned = false;
                enemyState = "Recovery";
                canAttack = true;
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

                if (debounce >= 1.5f)
                {
                    enemyState = "Moving";
                    debounce = 0;
                }
            }
            else if (enemyState == "Moving")
            {
                animator.SetBool("Move", true);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", false);

                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);


                if (distance <= 2 && distance >= -2)
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

                Attack(10, false);

                debounce += 1 * Time.deltaTime;
                if (debounce >= 1)
                {
                    enemyState = "Recovery";
                    debounce = 0;
                }
            }
            else if (enemyState == "Attack 2")
            {
                animator.SetBool("Move", false);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", true);

                Attack(30, false);

                debounce += 1 * Time.deltaTime;
                if (debounce >= 1)
                {
                    enemyState = "Recovery";
                    debounce = 0;
                }
            }
            else if (enemyState == "Recovery")
            {
                
                canAttack = true;
                animator.SetBool("Move", true);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", false);

                if (distance <= 6 && distance >= -6)
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

                if (distance >= 3 || distance <= -3)
                {
                    if (canShoot)
                    {
                      //  print("SHOT");
                        canShoot = false;

                        int roll = Random.Range(0, 5);

                        if (roll == 0)
                        {
                            Shoot();
                          //  print(roll);
                        }
                        else
                        {
                          //  print(roll);
                        }

                        
                    }
                }

                debounce += 1 * Time.deltaTime;
                if (debounce >= .8)
                {
                    canShoot = true;
                    enemyState = "Moving";
                    debounce = 0;
                }
            }

        }

    }
    

    public void Shoot()
    {
        GameObject bul = Instantiate(shootThing);
        if (GetTurn1() == true)
        {
            bul.transform.position = (transform.position + new Vector3(-2f, 1.5f, 0));

        }
        else if (GetTurn2() == true)
        {
            bul.transform.position = (transform.position + new Vector3(2f, 1.5f, 0));
        }
        else
        {
            bul.transform.position = (transform.position + new Vector3(-2f, 1.5f, 0));
        }
    }
}
