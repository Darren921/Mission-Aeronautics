using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EarthmanAI : Enemy
{

    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject fireBall;


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
                animator.SetBool("Attack 2", false);

                if (distance <= 5 && distance >= -5)
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

                if (debounce >= 1.5f)
                {
                    Shoot();
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
}
