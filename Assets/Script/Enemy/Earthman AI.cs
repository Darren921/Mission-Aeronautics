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
                enemyState = "Attack";
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
                enemyState = "Pre Attack 2";
            }
        }
        else if (enemyState == "Attack")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);

            if (distance >= 0)
            {
                if (distance < 5)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 5, this.transform.position.y), speed * Time.deltaTime);
                }
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
            }
            else
            {
                if (distance > -5)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 5, this.transform.position.y), speed * Time.deltaTime);
                }
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
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
                    enemyState = "Moving";
                    fireShot = 0;
                }
            }
        }
        else if (enemyState == "Pre Attack 2")
        {
            if (health.GetStunnedFire)
            {
                debounce = 0;
                enemyState = "Stunned";
            }
            else
            {
                debounce += 1 * Time.deltaTime;
                if (debounce >= 1.5)
                {
                    enemyState = "Attack 2";
                    debounce = 0;
                }
            }
        }
        else if (enemyState == "Attack 2")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);

            IgAttack(40, false);
            debounce += 1 * Time.deltaTime;
            if (debounce >= 1)
            {
                enemyState = "Recovery";
                debounce = 0;
            }

        }
        else if (enemyState == "Recovery")
        {
            animator.SetBool("Move", true);
            animator.SetBool("Stun", false);
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 10, this.transform.position.y), speed * Time.deltaTime);
            if (distance > 8)
            {
                enemyState = "Attack";
            }
        }
        else if(enemyState == "Stunned")
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
