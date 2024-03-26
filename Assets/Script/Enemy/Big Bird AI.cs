using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBirdAI : Enemy
{
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject birdBall;

    private int fireShot = 0;

    private Vector3 teleportLocation;

 
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
            animator.SetBool("Invisible", false);

            debounce += 1 * Time.deltaTime;

            if (debounce >= .25f)
            {
                enemyState = "Attack";
                debounce = 0;
            }
        }
        else if (enemyState == "Attack")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);
            animator.SetBool("Invisible", false);

            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, player.transform.position.y), speed * Time.deltaTime);

            if (distance < 10 && distance > -10)
            {
                if (enemy.GetTurn1())
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 10, player.transform.position.y), speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 10, player.transform.position.y), speed * Time.deltaTime);
                }

            }


            debounce += 1 * Time.deltaTime;

            if (debounce >= 2f)
            {
                Shoot();
                debounce = 0;

                fireShot += 1;
                if (fireShot >= 3)
                {
                    debounce = 0;
                    enemyState = "Pre Teleport";
                    fireShot = 0;
                }
            }
        }
        else if (enemyState == "Pre Teleport")
        {
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
            animator.SetBool("Invisible", true);

            debounce += 1 * Time.deltaTime;

            if (debounce >= 2)
            {
                enemyState = "Teleport";
                debounce = 0;
            }
        }
        else if (enemyState == "Teleport")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Invisible", false);

            this.transform.position = teleportLocation;

            enemyState = "After Teleport";
            debounce = 0;
        }
        else if (enemyState == "After Teleport")
        {
            if(distance > -10 && distance < 10)
            {
                BirbAttack(40, false);
            }
            else
            {
                enemyState = "Stunned";
            }

            debounce += 1 * Time.deltaTime;

            if (debounce >= 2)
            {
                enemyState = "Recovery";
            }
        }
        else if (enemyState == "Stunned")
        {
            Debug.Log("STUNNED");
            animator.SetBool("Move", false);
            animator.SetBool("Stun", true);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            stunDebounce += 1 * Time.deltaTime;
            if (stunDebounce >= 3)
            {
                health.GetStunnedFire = false;
                enemyState = "Recovery";
                canAttack = true;
                stunDebounce = 0;
                debounce = 0;
            }
        }
        else if(enemyState == "Recovery")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            if (enemy.GetTurn1())
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 20, this.transform.position.y), speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 20, this.transform.position.y), speed * Time.deltaTime);
            }

            if (distance > 16 || distance < -16)
            {
                enemyState = "Attack";
            }

        }
    }

    public void Shoot()
    {
        GameObject bul = Instantiate(birdBall);
        if (enemy.GetTurn1())
        {
            bul.transform.position = (transform.position + new Vector3(-4f, 1.5f, 0));
        }
        else
        {
            bul.transform.position = (transform.position + new Vector3(4f, 1.5f, 0));
        }
    }

    internal void BirbAttack(float damage, bool isProj)
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
            canAttack = true;
        }
    }
}
