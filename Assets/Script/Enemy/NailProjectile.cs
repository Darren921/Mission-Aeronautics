using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailProjectile : MonoBehaviour
{
    Enemy enemy;
    EvilDarrenAI evAI;
    private Rigidbody2D rb;
    private float time = 0;
    Player player;
    private float damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        evAI = FindObjectOfType<EvilDarrenAI>();
        damage = 5;
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
            if (player.returnShieldPowerActive() != true)
            {
                evAI.NailAttack(damage, true);
                Destroy(this.gameObject);
            }
            else
            evAI.NailAttack(0, true);
            Destroy(this.gameObject);

        }
    }
}
