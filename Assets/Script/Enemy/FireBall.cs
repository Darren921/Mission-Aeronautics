using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Enemy enemy;
    EarthmanAI emAI;
    private Rigidbody2D rb;
    private float time = 0;
    Player player;
    private float damage;

    private bool rotated = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        emAI = FindObjectOfType<EarthmanAI>();
        damage = 10;
    }

    void Update()
    {
        if (enemy.GetTurn1() == true)
        {
            if (rotated)
            {
                rotated = false;
                Debug.Log("ENEMY ROTATE: " + enemy.GetTurn1());
                this.gameObject.transform.Rotate(0, 180, 0, Space.Self);
            }

            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(-2500 * Time.deltaTime, 0));
        }
        if (enemy.GetTurn2() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(2500 * Time.deltaTime, 0));
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
                emAI.IgAttack(damage, true);
                Destroy(this.gameObject);
            }
            else
            {
                emAI.IgAttack(0, true);
                Destroy(this.gameObject);
            }
            
        }
    }
}
