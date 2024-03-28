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
            animator.SetBool("Stun", false);

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
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Stun", false);

            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);


            if (distance <= 3 && distance >= -3)
            {
                enemyState = "Prepare Attack";
                debounce = 0;
            }
        }
        else if (enemyState == "Prepare Attack")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Stun", false);

            if (health.GetStunnedFire)
            {
                debounce = 0;
                enemyState = "Stunned";
            }

            debounce += 1 * Time.deltaTime;
            
            if (debounce >= 0.2)
            {
                enemyState = "Grab";
                debounce = 0;
            }
        }
        else if (enemyState == "Grab")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Stun", false);

            if (distance >= 1.5 && distance <= -1.5)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }

            XerosAttack(20, false);

            debounce += 1 * Time.deltaTime;
            if (debounce >= .5)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
        else if (enemyState == "Stunned")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Stun", true);

            stunDebounce += 1 * Time.deltaTime;
            if (stunDebounce >= 1.5)
            {
                health.GetStunnedFire = false;
                enemyState = "Prepare Projectile";
                canAttack = true;
                stunDebounce = 0;
                debounce = 0;
            }
        }
        else if (enemyState == "Prepare Projectile")
        {
            animator.SetBool("Move", true);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Stun", false);

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
                enemyState = "Projectile Attack";
            }
        }
        else if (enemyState == "Projectile Attack")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", true);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Stun", false);

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

            if (debounce >= 1.5f)
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
            animator.SetBool("Teleport", true);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);
            animator.SetBool("Stun", false);

            debounce += 1 * Time.deltaTime;

            if (debounce >= 1.5)
            {
                enemyState = "Teleport";
                debounce = 0;
            }
        }
        else if (enemyState == "Teleport")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Teleport", false);
            animator.SetBool("Projectile", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);
            animator.SetBool("Stun", false);

            this.transform.position = teleportLocation;

            enemyState = "After Teleport";
            debounce = 0;
        }
        else if (enemyState == "After Teleport")
        {
            if (distance > -10 && distance < 10)
            {
                XerosAttack(30, false);
            }
            else
            {
                enemyState = "Move";
                debounce = 0;
            }

            debounce += 1 * Time.deltaTime;
            if(debounce >= 0.4)
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
        if (collidingWithPlayer)
        {
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
