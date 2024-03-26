using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XerosAI : Enemy
{
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject xerosShot;

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
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            debounce += 1 * Time.deltaTime;

            if (debounce >= .25f)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
        else if (enemyState == "Move")
        {
            canAttack = true;
            animator.SetBool("Move", true);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);


            if (distance <= 3 && distance >= -3)
            {
                int roll = Random.Range(0, 3);

                if (roll == 0)
                {
                    enemyState = "Stab";
                }
                else
                {
                    enemyState = "Grab";
                }
            }
        }
        else if (enemyState == "Grab")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);

            if (distance >= 1.5 && distance <= -1.5)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }

            XerosAttack(20, false);

            debounce += 1 * Time.deltaTime;
            if (debounce >= 1)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
        else if (enemyState == "Stab")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);

            if (distance >= 1.5 && distance <= -1.5)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }

            XerosAttack(30, false);

            debounce += 1 * Time.deltaTime;
            if (debounce >= 1)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
    }

    public void Shoot()
    {
        GameObject bul = Instantiate(xerosShot);
        if (enemy.GetTurn1())
        {
            bul.transform.position = (transform.position + new Vector3(-4f, 1.5f, 0));
        }
        else
        {
            bul.transform.position = (transform.position + new Vector3(4f, 1.5f, 0));
        }
    }

    internal void XerosAttack(float damage, bool isProj)
    {
        print("Attack Fired");
        if (collidingWithPlayer)
        {
            print("Collide Fired");
            if (canAttack)
            {
                print("Can Attack Fired");
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
