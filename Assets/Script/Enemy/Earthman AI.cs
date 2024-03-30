using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EarthmanAI : Enemy
{
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject fireBall;

    private int fireShot = 0;

    void Start()
    {
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

            if (debounce >= .25f)
            {
                enemyState = "Fire Attack";
                debounce = 0;
            }
        }
        else if (enemyState == "Fire Attack")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);

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
                source.PlayOneShot(AttackEffects[0]);
                Shoot();
                debounce = 0;

                fireShot += 1;
                if (fireShot >= 3)
                {
                    debounce = 0;
                    enemyState = "Move";
                    fireShot = 0;
                }
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

            else
            {
                debounce += 1 * Time.deltaTime;
                if (debounce >= .2)
                {
                    source.PlayOneShot(AttackEffects[1]);
                    enemyState = "Kick";
                    debounce = 0;
                }
            }
        }
        else if (enemyState == "Kick")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);

            IgAttack(30, false);
            debounce += 1 * Time.deltaTime;
            if (debounce >= 1)
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
        else if(enemyState == "Stunned")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", true);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            stunDebounce += 1 * Time.deltaTime;
            if (stunDebounce >= 2)
            {
                health.GetStunnedFire = false;
                enemyState = "Recovery";
                canAttack = true;
                stunDebounce = 0;
                debounce = 0;
            }
        }
        else if (enemyState == "Recovery")
        {
            animator.SetBool("Move", true);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            if (enemy.GetTurn1())
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 10, this.transform.position.y), speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 10, this.transform.position.y), speed * Time.deltaTime);
            }

            if (distance > 8 || distance < -8)
            {
                enemyState = "Fire Attack";
                debounce = 0;
            }

            if (enemyState == "Recovery")
            {
                debounce += Time.deltaTime;

                if (debounce >= 4)
                {
                    enemyState = "Fire Attack";
                    debounce = 0;
                }
            }
        }
    }

    public void Shoot()
    {
        GameObject bul = Instantiate(fireBall);
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

    internal void IgAttack(float damage, bool isProj)
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
