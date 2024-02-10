using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    [SerializeField] private Sprite[] enemySprites;

    
    [SerializeField] private float playerHealth;


    public static bool performed2;
    public static bool isColliding2;
    public static bool _isAttacking2;

    [SerializeField] private Slider playerHealthBarSlider;

    private bool canAttack = true;

    private float time = 0f;

    private float distance;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = 100;
    }


    void Update()
    {
        playerHealthBarSlider.value = playerHealth;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position + new Vector3(2, 0, 0), speed * Time.deltaTime);

        if (transform.position == player.transform.position + new Vector3(2, 0, 0))
        {
            if (canAttack)
            {
                canAttack = false;
                _isAttacking2 = true;
                Attacking();
                playerHealth -= 10;
                _isAttacking2 = false;
            }
        }
        else
        {
            canAttack = true;
            time = 0f;
        }

        if (canAttack == false)
        {
            time += Time.deltaTime;
            if (time >= 2)
            {
                canAttack = true;
                time = 0f;
            }
        }
    }

    private void Attacking()
    {
        StartCoroutine(AttackCheck());
        if (isColliding2)
        {
            playerHealth -= 10;
        }
        StopCoroutine(AttackCheck());
    }
    IEnumerator AttackCheck()
    {
        performed2 = true;
        _isAttacking2 = true;
        print("Performed");
        this.GetComponent<SpriteRenderer>().sprite = enemySprites[1];
        yield return new WaitForSeconds(0.2f);
        _isAttacking2 = false;
        performed2 = false;
        this.GetComponent<SpriteRenderer>().sprite = enemySprites[0];
        StopCoroutine(AttackCheck());
    }


    public bool enemyPerforming()
    {
        return performed2;
    }

    public bool enemyColliding()
    {
        return isColliding2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding2 = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding2 = false;
        }
    }
}
