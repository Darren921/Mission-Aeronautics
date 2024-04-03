using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBall : MonoBehaviour
{
    Enemy enemy;
    BigBirdAI bbAi;
    private Rigidbody2D rb;
    private float time = 0;
    Player player;
    private float damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        bbAi = FindObjectOfType<BigBirdAI>();
        damage = 10;
    }

    void Update()
    {
        if (enemy.GetTurn1() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(-4000 * Time.deltaTime, 0));
        }
        if (enemy.GetTurn2() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(4000 * Time.deltaTime, 0));
        }

        if (time >= 1.5f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(player.returnShieldPowerActive() != true)
            {
                bbAi.BirbAttack(damage, true);
                Destroy(this.gameObject);
            }
            else
            {
                bbAi.BirbAttack(0, true);
                Destroy(this.gameObject);
            }
        }
    }
}
