using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XerosProjectile : MonoBehaviour
{
    Enemy enemy;
    XerosAI xeros;
    private Rigidbody2D rb;
    private float time = 0;

    private float damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = FindObjectOfType<Enemy>();

        xeros = FindObjectOfType<XerosAI>();
        damage = 50;
    }

    void Update()
    {
        if (enemy.GetTurn1() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(-3000 * Time.deltaTime, 0));
        }
        if (enemy.GetTurn2() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(3000 * Time.deltaTime, 0));
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
            // Attack here
            Destroy(this.gameObject);
        }
    }
}
