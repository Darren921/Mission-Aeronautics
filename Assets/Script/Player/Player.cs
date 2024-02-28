using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PowerUps;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveDir;
    private Vector2 _smoothedMoveDir;
    private Vector2 _smoothedMoveVelocity;
   
    private bool FirstMove;

    private static bool _isAttacking;
    public static bool performed;
    public static bool isColliding;
    private static bool isSpecialAtk;
    private static bool isCollecting;

    private bool isShooting;
   
    private Vector3 EndPosA;


    /*
    private int moveNumber;
    private Vector3 EndPosBoost;
     private bool GravActive;
    private PowerUpType collectedPowerUp;
    */

    [SerializeField] private Animator animator;
    [SerializeField] private Health _health;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private BrickManAI aI;
    [SerializeField] private PowerUps powerUps;
    private GameObject bullet;
    private float bulletDestroy = 0;

    // Start is called before the first frame update
    void Start()
    {
        //GravActive = true;
        animator = GetComponent<Animator>();
        switch (Buttons.CharacterChossen)
        {
            case 1:
                animator.runtimeAnimatorController = animatorOverrideControllers[0];
                AttackEffects = Resources.LoadAll<AudioClip>("Characters Sprites/Player/Zhinc/Attack sounds");
                bullet = projectiles[0];
                break;
            case 2:
                animator.runtimeAnimatorController = animatorOverrideControllers[1];
                AttackEffects = Resources.LoadAll<AudioClip>("Characters Sprites/Player/Tabor/Attack sounds");
                bullet = projectiles[1];
                break;
            case 3:
                animator.runtimeAnimatorController = animatorOverrideControllers[2];
                bullet = projectiles[2];
                AttackEffects = Resources.LoadAll<AudioClip>("Characters Sprites/Player/Biofuse/Attack sounds");
                break;
            case 4:
                AttackEffects = Resources.LoadAll<AudioClip>("Characters Sprites/Player/Hammer/Attack sounds");
                break;

        }



        //Activating the InputManager and Controls
        // InputManager.InitM(this, this);

        // moveNumber = 0;
        InputManager.InitS(this);
        InputManager.EnableInGame();
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
        
        if(transform.position.y < -20.35547 || transform.position.x < -38.63747 || transform.position.y > 20.35547 || transform.position.x > 38.63747)
        {
            Destroy(GameObject.Find("Player"));
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
   

    public void CollectPowerUp()
    {
        isCollecting = true;
        if (CollectingPowerUP() != null)
        {
            StopCoroutine(CollectingPowerUP());
        }
        StartCoroutine(CollectingPowerUP());
        IEnumerator CollectingPowerUP()
        {
            yield return new WaitForSeconds(2f);
            isCollecting = false;
        }
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
                source.PlayOneShot(AttackEffects[0]);
                yield return new WaitForSeconds(1f);
                animator.SetInteger("MoveNumber", 1);
                break; 
            case 1:
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 1);
                source.PlayOneShot(AttackEffects[1]);
                yield return new WaitForSeconds(1f);
                animator.SetInteger("MoveNumber", 2);
                break;
            case 2:
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 1);
                source.PlayOneShot(AttackEffects[2]);
                yield return new WaitForSeconds(1f);
                animator.SetInteger("MoveNumber", 0);
                break;
            case 3:
                EndPosA = transform.position + new Vector3(0.2f, 0, 0);
                transform.position = Vector3.Lerp(transform.position, EndPosA, 1);
                source.PlayOneShot(AttackEffects[3]);
                yield return new WaitForSeconds(1f);
                animator.SetInteger("MoveNumber", 0);
                break;
            default:
                break;

        }
        if (isSpecialAtk == true)
        {
            yield return new WaitForSeconds(0.6f);
            isSpecialAtk = false;
            _isAttacking = false;
        }

        _isAttacking = false;
        animator.SetBool("IsAttacking", false);
        performed = false;

       
        StopCoroutine(AttackCheck());


    }
    public bool ReturnFirstMove()
    {
        return FirstMove;
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

   public bool IsCollecting()
    {
        return isCollecting;
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

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = true;
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            switch (powerUps.ReturnPowerUpTypeS())
            {
                case "Health":
                    if (aI.playerHealth <= 75)
                    {
                        aI.playerHealth += 20;
                        aI.playerSlider.value = aI.playerHealth;
                    }
                    else if (aI.playerHealth >= 75)
                    {
                        aI.playerHealth = 75;
                        aI.playerSlider.value = aI.playerHealth;
                    }
                    break;
                case "Damage":
                    _health.damage *= 2;
                    break;
                case "Shield":
                    aI.playerHealth = aI.playerHealth += aI.Attack(0);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
    */



    public void Shoot()
    {
        GameObject bul =  Instantiate(bullet);
        bul.transform.position = (transform.position + new Vector3(2f, 2.5f, 0));
    }

}
