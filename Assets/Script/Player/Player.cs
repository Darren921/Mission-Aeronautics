using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _moveDir;
    private int moveNumber;
    private static bool _isAttacking;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Sprite[] playerSprites;
    private Vector3 EndPosA;
    private Vector3 EndPosBoost;
    SpriteRenderer spriteRenderer;

    private bool GravActive;
    private bool isMoved;

    public static bool performed;
    public static bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        GravActive = true;
        PlayerAssigment playerAssigment = GameObject.Find("PlayerAssigment").GetComponent<PlayerAssigment>();
        spriteRenderer  = GetComponent<SpriteRenderer>();

        switch (Buttons.CharacterChossen) 
        {
            case 1:
                spriteRenderer.sprite = playerAssigment._defaultSprites[0];
                playerSprites = Resources.LoadAll<Sprite>("Characters Sprites/Zhinc");
                break;
            case 2:
                
                spriteRenderer.sprite = playerAssigment._defaultSprites[1];
                playerSprites = Resources.LoadAll<Sprite>("Characters Sprites/Bilal");
                break;
            case 3:
                spriteRenderer.sprite = playerAssigment._defaultSprites[2];
                playerSprites = Resources.LoadAll<Sprite>("Characters Sprites/Ali");
                break;
            case 4:
               
                spriteRenderer.sprite = playerAssigment._defaultSprites[3];
                playerSprites = Resources.LoadAll<Sprite>("Characters Sprites/Hammer 1");

                break;

        }




        //Activating the InputManager and Controls
        // InputManager.InitM(this, this);
        InputManager.InitS(this);
        InputManager.EnableInGame();
        moveNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoved == true)
        {
            isMoved = true;
        }
        if (isMoved == true)
        {
            Gravity();
        }
        transform.position += (Vector3)(_moveSpeed * Time.fixedDeltaTime * _moveDir);
        
     


    }



    public void SetMoveDirection(Vector2 newDir)
    {
        _moveDir = newDir;
        isMoved = true;
    }
    public void Attacking()
    {
        isMoved = true;
        if (_isAttacking == true)
        {
            return;
        }
        StartCoroutine(AttackCheck());
    }

   
    IEnumerator AttackCheck()
    {
        performed = true;
        _isAttacking = true;
     
         
        

        switch (moveNumber) 
        { 
            case 0:
                spriteRenderer.sprite = playerSprites[1];
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 0);
                yield return new WaitForSeconds(0.6f);
                moveNumber = 1;
                break; 
            case 1:
                spriteRenderer.sprite = playerSprites[2];
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 0);
                yield return new WaitForSeconds(0.6f);
                moveNumber = 0;
                break;
        }
        _isAttacking = false;
        performed = false;

        spriteRenderer.sprite = playerSprites[0];
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

    public void Gravity ()
    {
        if (GravActive == true)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.009f);
        }
        else return;
    }

    public void Boost()
    {
        isMoved = true;
        StartCoroutine(Boosting());
    } 

    IEnumerator Boosting()
    {
        GravActive = false;
        yield return null;
        EndPosBoost = transform.position + new Vector3(0, 3f, 0);
        transform.position = Vector3.Lerp(transform.position, EndPosBoost, 2);
        yield return new WaitForSeconds(0.5f);
        GravActive = true;
        StopCoroutine(Boosting());
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
