using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _moveDir;
    private int moveNumber;
    private static bool _isAttacking;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Sprite[] playerSprites;
    private Vector3 EndPos;


    public static bool performed;
    public static bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //Activating the InputManager and Controls
        // InputManager.InitM(this, this);
        InputManager.InitS(this);
        InputManager.EnableInGame();
        moveNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(_moveSpeed * Time.fixedDeltaTime * _moveDir); 
     
    }
    private void FixedUpdate()
    {

        if (_isAttacking == true)
        {
            transform.position = Vector3.Lerp(transform.position, EndPos, 2);
        }
    }
    public void SetMoveDirection(Vector2 newDir)
    {
        _moveDir = newDir;
    }
    public void Attacking()
    {
        if (_isAttacking == true)
        {
            return;
        }
        StartCoroutine(AttackCheck());
        EndPos = transform.position + new Vector3(0.2f, 0, 0);
        StopCoroutine(AttackCheck());
    }

   
    IEnumerator AttackCheck()
    {
        performed = true;
        _isAttacking = true;


        switch (moveNumber) 
        { 
            case 0:
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[1];
                yield return new WaitForSeconds(0.75f);
                moveNumber = 1;
                break; 
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = playerSprites[2];
                yield return new WaitForSeconds(0.75f);
                moveNumber = 0;
                break;
        }
        _isAttacking = false;
        performed = false;

        this.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
        StopCoroutine(AttackCheck());


    }

    public bool performing()
    {
        return performed;
    }

    public bool colliding()
    {
        return isColliding;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }


}
