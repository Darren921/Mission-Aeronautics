using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private  Enemy enemy;
    private Rigidbody2D rb;
    private float time = 0;

    private Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        enemy = FindObjectOfType<Enemy>();

        health = FindObjectOfType<Health>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Tutorial.tutFin != true)
        {
            if (enemy != null)
            {

                if (enemy.GetTurn1() == true)
                {
                    rb.AddForce(new Vector2(3000 * Time.deltaTime, 0));
                }
                if (enemy.GetTurn2() == true)
                {
                    rb.AddForce(new Vector2(-3000 * Time.deltaTime, 0));
                }



            }
            else
            {
                rb.AddForce(new Vector2(3000 * Time.deltaTime, 0));

            }
        }
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
   
    
}
