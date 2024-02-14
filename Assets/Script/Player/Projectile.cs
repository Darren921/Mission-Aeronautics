using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float time = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += 1 * Time.deltaTime;
        rb.AddForce(new Vector2(3000 * Time.deltaTime, 0)) ;
        if (time >= 1.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
