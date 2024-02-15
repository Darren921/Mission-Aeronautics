using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveDir;
    private Vector2 _smoothedMoveDir;
    private Vector2 _smoothedMoveVelocity;
    private bool GravActive;
    private bool FirstMove;

    private int moveNumber;
    private static bool _isAttacking;
    public static bool performed;
    public static bool isColliding;
    private static bool isSpecialAtk;

    private bool isShooting;

    private Vector3 EndPosA;
    private Vector3 EndPosBoost;

    [SerializeField] private Animator animator;
    [SerializeField] private Health _health;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;

    private GameObject bullet;
    private float bulletDestroy = 0;

    // Start is called before the first frame update
    void Start()
    {
        GravActive = true;
        animator = GetComponent<Animator>();
        //_health = GameObject.Find("PlayerHealthBar").GetComponent<Health>(); (disabled because the below code didnt work)
        switch (Buttons.CharacterChossen)
        {
            case 1:
                animator.runtimeAnimatorController = animatorOverrideControllers[0];
                bullet = projectiles[0];
                break;
            case 2:
                animator.runtimeAnimatorController = animatorOverrideControllers[1];
                bullet = projectiles[1];
                break;
            case 3:
                animator.runtimeAnimatorController = animatorOverrideControllers[2];
                bullet = projectiles[2];
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
        
        if (animator.GetBool("IsMoving") == false)
        {
            StartCoroutine(IsIdle());
        }
        if (FirstMove == true)
        {
            FirstMove = true;
        }
        if (FirstMove == true)
        {
            //Gravity();
        }
        if (_moveDir == new Vector2(0, 0))
        {
            animator.SetBool("IsMoving", false);
        }
        if(transform.position.y < -15.8)
        {
            Destroy(this);
            animator.enabled = false;
        }
        if (isSpecialAtk == true)
        {
            performed = true;
        }
       
    }
    private void FixedUpdate()
    {
        _smoothedMoveDir = Vector2.SmoothDamp(_smoothedMoveDir, _moveDir, ref _smoothedMoveVelocity, 0.1f);
        rb.velocity = _smoothedMoveDir * _moveSpeed;
    }


    public void SetMoveDirection(Vector2 newDir)
    {
        _moveDir = newDir;
        animator.SetBool("IsMoving", true);
        animator.SetBool("isIdle", false);
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
                animator.SetInteger("MoveNumber", 2);
                break;
            case 2:
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 1);
                yield return new WaitForSeconds(0.6f);
                animator.SetInteger("MoveNumber", 0);
                break;
            case 3:
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 1);
                yield return new WaitForSeconds(0.6f);
                animator.SetInteger("MoveNumber", 0);
                break;
            default:
                break;

        }
        if (isSpecialAtk == true)
        {
            yield return new WaitForSeconds(0.6f);
            isSpecialAtk = false;
        }

        _isAttacking = false;
        animator.SetBool("IsAttacking", false);
        performed = false;

       
        StopCoroutine(AttackCheck());


    }

    public void SpecialAttack()
    {
        
        if (_health.GetCombo() >= 5)
        {
            animator.SetInteger("MoveNumber", 3);
            isSpecialAtk = true;
            Attacking();
        }
        else
        {
            return;
        }
        
    }

    public bool SpecialAtk()
    {
        return isSpecialAtk;
    }

    public bool Performing()
    {
        return performed;
    }

    public bool Colliding()
    {
        return isColliding;
    }

    IEnumerator IsIdle()
    {
        yield return new WaitForSeconds(3f); 
        animator.SetBool("isIdle", true);
    }

   


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = false;
        }
    }
    /*
    public void Gravity()
    {
        if (GravActive == true)
        {
            this.transform.position = new Vector2(transform.position.x, transform.position.y - 0.010f);
        }
        else return;
    }

    public void Boost()
    {
        FirstMove = true;
        StartCoroutine(Boosting());
    }
    */

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
    
    public void Shoot()
    {
        GameObject bul =  Instantiate(bullet);
        bul.transform.position = (transform.position + new Vector3(2f, 2.5f, 0));
    }

}
