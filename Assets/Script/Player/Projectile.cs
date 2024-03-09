using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Enemy enemy;
    private Rigidbody2D rb;
    private float time = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        enemy = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetTurn1() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(3000 * Time.deltaTime, 0));
        }
        if (enemy.GetTurn2() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(-3000 * Time.deltaTime, 0));
        }

        if (time >= 1.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionTrigger2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
