using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBirdAI : Enemy
{
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject birdBall;

    private int fireShot = 0;


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


                if (distance <= 5 && distance >= -5)
                {
                    enemyState = "Attack";
                }
            }
            else if (enemyState == "Attack")
            {
                animator.SetBool("Move", false);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", true);

                if (distance <= 5 && distance >= -5)
                {
                    if (distance >= 0)
                    {
                        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 5, player.transform.position.y), speed * Time.deltaTime);
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 5, player.transform.position.y), speed * Time.deltaTime);
                    }
                }
                else
                {
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
                        enemyState = "Recovery";
                    }
                }
            }
            else if (enemyState == "Recovery")
            {
                animator.SetBool("Move", false);
                animator.SetBool("Stun", false);
                animator.SetBool("Attack 1", false);
                animator.SetBool("Attack 2", false);

                debounce += 1 * Time.deltaTime;
                if (debounce >= 5)
                {
                    enemyState = "Attack";
                }
            }
        }
    }

    public void Shoot()
    {
        GameObject bul = Instantiate(birdBall);
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
