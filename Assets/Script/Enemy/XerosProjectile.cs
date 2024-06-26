using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class XerosProjectile : MonoBehaviour
{
    Enemy enemy;
    XerosAI xeros;
    private Rigidbody2D rb;
    private float time = 0;
    private float damage;
    private Player player;
    private bool rotated = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        xeros = FindObjectOfType<XerosAI>();
        damage = 4;
    }

    void Update()
    {
        if (enemy.GetTurn1() == true)
        {
            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(-9000 * Time.deltaTime, 0));
        }
        if (enemy.GetTurn2() == true)
        {
            if (rotated)
            {
                rotated = false;
                Debug.Log("ENEMY ROTATE: " + enemy.GetTurn1());
                this.gameObject.transform.Rotate(0, 180, 0, Space.Self);
            }

            time += 1 * Time.deltaTime;
            rb.AddForce(new Vector2(9000 * Time.deltaTime, 0));
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
                xeros.XerosAttack(damage, true);
                Destroy(this.gameObject);
            }
            else
            {
                xeros.XerosAttack(0, true);
                Destroy(this.gameObject);
            }
            
        }
    }
}
