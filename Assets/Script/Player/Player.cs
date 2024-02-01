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
    private Vector3 EndPosA;
    private Vector3 EndPosBoost;
    private Animator animator;

    private bool GravActive;
    private bool FirstMove;

    public static bool performed;
    public static bool isColliding;
    [SerializeField]private AnimatorOverrideController[] animatorOverrideControllers;

    // Start is called before the first frame update
    void Start()
    {

        GravActive = true;
        PlayerAssigment playerAssigment = GameObject.Find("PlayerAssigment").GetComponent<PlayerAssigment>();
        animator = GetComponent<Animator>();
        switch (Buttons.CharacterChossen)
        {
            case 1:
                animator.runtimeAnimatorController = animatorOverrideControllers[0];
                break;
            case 2:
                animator.runtimeAnimatorController = animatorOverrideControllers[1];
                break;
            case 3:
                animator.runtimeAnimatorController = animatorOverrideControllers[2];
                break;
            case 4:
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
        if (FirstMove == true)
        {
            FirstMove = true;
        }
        if (FirstMove == true)
        {
            Gravity();
        }
        transform.position += (Vector3)(_moveSpeed * Time.deltaTime * _moveDir);
        if (_moveDir == new Vector2(0, 0))
        {
            animator.SetBool("IsMoving", false);
        }
        if(transform.position.y < -15.8)
        {
            Destroy(this);
            animator.enabled = false;
        }



    }



    public void SetMoveDirection(Vector2 newDir)
    {
        _moveDir = newDir;
        animator.SetBool("IsMoving", true);
        FirstMove = true;
    }
    public void Attacking()
    {
        FirstMove = true;
        animator.SetBool("IsAttacking", true);

        if ( _isAttacking == true)
        {
            animator.SetBool("IsAttacking", false);
            return;
        }
        StartCoroutine(AttackCheck());
    }

   
    IEnumerator AttackCheck()
    {
        performed = true;
        _isAttacking = true;

        animator.SetBool("IsAttacking", true);



        switch (animator.GetInteger("MoveNumber")) 
        { 
            case 0:
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 1);
                yield return new WaitForSeconds(0.6f);
                animator.SetInteger("MoveNumber", 1);
                break; 
            case 1:
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 1);
                yield return new WaitForSeconds(0.6f);
                animator.SetInteger("MoveNumber", 0);

                break;
        }
        _isAttacking = false;
        animator.SetBool("IsAttacking", false);
        performed = false;

       
        StopCoroutine(AttackCheck());


    }

    public bool Performing()
    {
        return performed;
    }

    public bool Colliding()
    {
        return isColliding;
    }

    public void Gravity ()
    {
        if (GravActive == true)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.001f);
        }
        else return;
    }

    public void Boost()
    {
        FirstMove = true;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }


}
