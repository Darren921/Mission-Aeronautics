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

            if (debounce >= .6f)
            {
                source.PlayOneShot(AttackEffects[0]);
                Shoot();
                debounce = 0;

                fireShot += 1;
                if (fireShot >= 5)
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

            if (debounce >= 1)
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

            source.PlayOneShot(AttackEffects[1]);

            this.transform.position = teleportLocation;

            enemyState = "After Teleport";
            fireShot = 0;
            debounce = 0;
        }
        else if (enemyState == "After Teleport")
        {
            debounce += 1 * Time.deltaTime;

            if (debounce >= .2f)
            {
                Shoot();
                debounce = 0;

                fireShot += 1;
            }

            if (fireShot >= 1)
            {
                debounce = 0;
                enemyState = "After Teleport Done";
            }
        }
        else if (enemyState == "After Teleport Done")
        {
            if (health.GetStunnedFire)
            {
                fireShot = 0;
                debounce = 0;
                stunDebounce = 0;
                enemyState = "Stunned";
            }

            stunDebounce += 1 * Time.deltaTime;

            if (stunDebounce >= 3)
            {
                fireShot = 0;
                enemyState = "Recovery";
                debounce = 0;
                stunDebounce = 0;
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

                int e = Random.Range(0, 3);

                if (e == 2)
                {
                    canMoveUp = true;
                }
            }
        }
        else if(enemyState == "Recovery")
        {
            fireShot = 0;
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

            if (distance > 13 || distance < -13)
            {
                enemyState = "Attack";
                debounce = 0;
            }

            if (canMoveUp)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y + 10), speed * Time.deltaTime);
            }

            if (enemyState == "Recovery")
            {
                canMoveUp = false;
                debounce += Time.deltaTime;

                if (debounce >= 4)
                {
                    enemyState = "Attack";
                    debounce = 0;
                }
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
        print(player.GetComponent<Player>().returnShieldPowerActive());
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
}
