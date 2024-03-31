using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvilDarrenAI : Enemy
{
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject shootThing;

    private int bulShot = 0;

    private bool canShoot = true;
    private bool canMoveUp = false;



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

            debounce += 1 * Time.deltaTime;

            if (debounce >= 1.5f)
            {
                enemyState = "Move";
                debounce = 0;
            }
        }
        else if (enemyState == "Move")
        {
            animator.SetBool("Move", true);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);

            if (distance <= 2 && distance >= -2)
            {
                enemyState = "Prepare Kick";
            }
        }
        else if (enemyState == "Prepare Kick")
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
                enemyState = "Kick";
                debounce = 0;
            }
        }
        else if (enemyState == "Kick")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);

            if (distance >= 1.5 && distance <= -1.5)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }

            NailAttack(10, false);

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

            if (enemy.GetTurn1())
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 20, this.transform.position.y), speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 20, this.transform.position.y), speed * Time.deltaTime);
            }

            if (canMoveUp)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y + 10), speed * Time.deltaTime);
            }
            

            if (distance > 12 || distance < -12)
            {
                enemyState = "Projectile";
                debounce = 0;
                canMoveUp = false;
            }

            if (enemyState == "Recover")
            {
                debounce += Time.deltaTime;

                if (debounce >= 3)
                {
                    enemyState = "Projectile";
                    debounce = 0;
                    canMoveUp = false;
                }
            }
        }
        else if (enemyState == "Projectile")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);

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
                source.PlayOneShot(AttackEffects[1]);
                Shoot();
                debounce = 0;

                bulShot += 1;
                if (bulShot >= 2)
                {
                    debounce = 0;
                    enemyState = "Move";
                    bulShot = 0;
                }
            }
        }
    }

    internal void NailAttack(float damage, bool isProj)
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

    public void Shoot()
    {
        GameObject bul = Instantiate(shootThing);
        if (enemy.GetTurn1())
        {
            bul.transform.position = (transform.position + new Vector3(-4f, 1.5f, 0));
        }
        else
        {
            bul.transform.position = (transform.position + new Vector3(4f, 1.5f, 0));
        }
    }
}
