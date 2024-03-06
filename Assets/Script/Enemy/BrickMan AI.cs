using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrickManAI : Enemy
{
    

    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        health = new Health();
        enemy = new Enemy();
        animator = GetComponent<Animator>();
        enemyState = "Moving";
        canAttack = true;
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

                Attack(10);

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

                Attack(30);

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

<<<<<<< Updated upstream
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }

   internal void Attack(float damage)
    {
        if (collidingWithPlayer)
        {
            if (canAttack)
            {
                if(player.GetComponent<Player>().returnisBlocking() != true) 
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
=======
   
>>>>>>> Stashed changes

    
    
}
