using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickManAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private Animator animator;

    private float distance;

    public string enemyState = "Idle";




    private float debounce = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyState = "Moving";
    }

    // Update is called once per frame
    void Update()
    {
        distance = (transform.position.x - player.transform.position.x);
        if (enemyState == "Idle")
        {
            animator.SetBool("Move", false);
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
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);

            if (distance <= 3)
            {
                int roll = Random.Range(0, 2);

                if (roll == 0)
                {
                    enemyState = "Attack 1";
                }
                else
                {
                    enemyState = "Attack 2";
                }
            }
        }
        else if (enemyState == "Attack 1")
        {
            animator.SetBool("Move", false);
            animator.SetBool("Attack 1", true);
            animator.SetBool("Attack 2", false);

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
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", true);

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
            animator.SetBool("Attack 1", false);
            animator.SetBool("Attack 2", false);

            if (distance <= 5)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 10, this.transform.position.y), speed * Time.deltaTime);
            }

            debounce += 1 * Time.deltaTime;
            if (debounce >= 2)
            {
                enemyState = "Moving";
                debounce = 0;
            }
        }
    }
}
