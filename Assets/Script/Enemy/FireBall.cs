using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Enemy enemy;
    Enemy enemy2;
    private Rigidbody2D rb;
    private float time = 0;

    private float damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        enemy = FindObjectOfType<Enemy>();

        switch (LevelPick.LevelChossen)
        {
            case 0:
                enemy2 = FindObjectOfType<TrainingDummy>();
                break;
            case 1:
                enemy2 = FindObjectOfType<BrickManAI>();
                break;
            case 2:
                enemy2 = FindObjectOfType<EvilDarrenAI>();
                damage = 10;
                break;
            case 3:
                enemy2 = FindObjectOfType<EarthmanAI>();
                damage = 40;
                break;

        }
    }

    // Update is called once per frame
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
            enemy2.Attack(damage, true);
            Destroy(this.gameObject);
        }
    }
}
