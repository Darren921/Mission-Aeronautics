using System;
using System.Collections;
using UnityEngine;
using static PowerUps;
using static PowerUpSpawner;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector2 _moveDir;
    private Vector2 _smoothedMoveDir;
    private Vector2 _smoothedMoveVelocity;
    private bool GravActive;
    private bool FirstMove;
    private PowerUpType collectedPowerUp;
   

    private int moveNumber;
    private static bool _isAttacking;
    public static bool performed;
    public static bool isColliding;
    private static bool isSpecialAtk;
    private static bool isCollecting;
    private static bool isCollected;
    private static bool isHit;
    private static bool isStunned;
    private static float stunCD;
    private static int Temphealth;
    private bool isBlocking;


    private bool isShooting;

    private Vector3 EndPosA;
    private Vector3 EndPosBoost;
    private Animator animator;
    [SerializeField] private Health _health;
    private Tutorial tut;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;
    [SerializeField] private AudioClip[] AttackEffects;
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject tutEnemy;
    private Enemy enemy;

    private PowerUpSpawner powerUpsSpawner;
    private GameObject bullet;
    private float bulletDestroy = 0;
    private TutTextManager textManager;
    private bool shieldPowerActive;

    private void OnDestroy()
    {
        InputManager.DisableInGame();
    }
    private void Awake()
    {
        textManager = FindObjectOfType<TutTextManager>();
        tut = FindObjectOfType<Tutorial>();
        if (Tutorial.tutFin != true)
        {
            InputManager.InitTut(this);
            InputManager.DisableInGame();

            InputManager.EnableInGame();
        }
        else
        {
            InputManager.InitS(this);
            InputManager.EnableInGame();
            

        }
    }


            
   public bool GetStunned 
    {
        get { return isStunned; }
        set { isStunned = value; }
    }


    void Start()
    {
        if (Tutorial.tutFin == true)
        {
            powerUpsSpawner = FindObjectOfType<PowerUpSpawner>();
            switch (LevelPick.LevelChossen)
            {
                case 1:
                    enemy = FindObjectOfType<EvilDarrenAI>();
                    break;
                case 2:
                    enemy = FindObjectOfType<BrickManAI>();
                    break;
                case 3:
                    enemy = FindObjectOfType<EarthmanAI>();
                    break;
                case 4:
                    enemy = FindObjectOfType<BigBirdAI>();
                    break;
                case 5:
                    enemy = FindObjectOfType<XerosAI>();
                    break;
            }
            
        }
        else
        {
             enemy = tutEnemy.GetComponent<TrainingDummy>();
            powerUpsSpawner = FindObjectOfType<PowerUpSpawner>();
        }
       


        GravActive = true;
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
                AttackEffects = Resources.LoadAll<AudioClip>("Characters Sprites/Player/Biofuse/Attack sounds");
                break;
            case 4:
                bullet = projectiles[2];
                AttackEffects = Resources.LoadAll<AudioClip>("Characters Sprites/Player/Hammer/Attack sounds");
                break;

        }



        //Activating the InputManager and Controls
        // InputManager.InitM(this, this);
     
        moveNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Tutorial.tutFin != true)
        {


            if (tut.refresh == true)
            {
                InputManager.InitTut(this);
                InputManager.DisableInGame();
                InputManager.EnableInGame();
                tut.refresh = false;
            }
           

             
        }
       

        if (animator.GetBool("IsMoving") == false)
        {
            StartCoroutine(IsIdle());
        }
        if (_moveDir == new Vector2(0, 0))
        {
            animator.SetBool("IsMoving", false);
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
        if (isCollecting)
            return;
        isCollecting = true;
        StartCoroutine(ResetP());
    }
    public void SetMoveDirection(Vector2 newDir)
    {
        _moveDir = newDir;
        if (animator.runtimeAnimatorController == null)
        {
            gameObject.GetComponent<Animator>();
        }
        animator.SetBool("IsMoving", true);
        animator.SetBool("isIdle", false);
        FirstMove = true;
    }
    public void Attacking()
    {
        if(isBlocking == true || isStunned == true)
        {
            return;
        }
        if (animator.GetBool("IsAttacking") == true)
        {
            return;
        }
        else
        {
            animator.SetBool("IsAttacking", true);
        }
        StartCoroutine(AttackCheck());
    }

   
    IEnumerator AttackCheck()
    {
        performed = true;
        _isAttacking = true;
        animator.SetBool("IsAttacking", true);
        if(isStunned !=  true )
        {
            
            switch (animator.GetInteger("MoveNumber") )
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
        }
        else
        {
            yield break;
        }
        if (isSpecialAtk == true )
        {
            if (Buttons.CharacterChossen == 1 || Buttons.CharacterChossen == 2)
            {
                StartCoroutine(Shoot(1));
            }
            if (Buttons.CharacterChossen == 4)
            {
                StartCoroutine(Shoot(3));
            }
            yield return new WaitForSeconds(1f);
            isSpecialAtk = false;

        }
        /*
        else if (isStunned == true)
        {

            {
                isSpecialAtk = false;
                _isAttacking = false;
                _health.damage = 0;
            }
        }
        */



        _isAttacking = false;
        animator.SetBool("IsAttacking", false);
        performed = false;

    }

    IEnumerator IsIdle()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("isIdle", true);
    }

   
    

    public void hitCheck ()
    {
        if (Tutorial.tutFin == true)
        {
            if (enemy.ReturnplayerHit() == true && Health.gameEnded == false)
            {
                StartCoroutine(stunCheck());
            }
        }
        if(Tutorial.tutFin == false)
        {
            if (Tutorial.tutFin !=  true && tut.block == true)
            {
                if (enemy.ReturnplayerHit() == true)
                {
                    StartCoroutine(stunCheck());
                }
            }
        }
    }

    

    public IEnumerator stunCheck()
    {
        if (isStunned == true )
        {
            StopCoroutine(stunCheck());

        }

        if (animator.GetBool("Guarding") ==  false)
        {
            isStunned = true;
            animator.SetBool("Stunned", true);
            gameObject.GetComponent<Player>().enabled = false;
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<Player>().enabled = true;
            animator.speed = 1;
            animator.SetBool("Stunned", false);
            isStunned = false;
        }
        else if (animator.GetBool("Guarding") == true) 
        {
            isStunned = false;
            animator.SetBool("Stunned", false);
        }
    }

    public void Blocking()
    {
        if (isStunned == true)
        {
            return;
        }
      
        StartCoroutine(Delay());
        isBlocking = true;
        animator.SetBool("Guarding", true);
        isStunned = false;
        rb.velocity = Vector2.zero;
        gameObject.GetComponent<Player>().enabled = false;

       
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
    }

    public void BlockCanceled()
    {
        gameObject.GetComponent<Player>().enabled = true;
        isBlocking = false;
        animator.SetBool("Guarding", false);
        animator.speed = 1;
        return;
    }

    public void StopAnimation()
    {
        animator.speed = 0;
    }

    

    public bool ReturnFirstMove()
    {
        return FirstMove;
    }
    public void SpecialAttack()
    {
        
        if (_health.GetCombo >= 5)
        {
            animator.SetInteger("MoveNumber", 3);
            if(isStunned!= true)
            {
                isSpecialAtk = true;
            }
            else
            {
                isSpecialAtk = false;
            }
            _health.GetCombo = 0;
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
    public bool returnisBlocking()
    {
        return isBlocking;
    }
  
    public bool returnShieldPowerActive()
    {
        return shieldPowerActive;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        print(isCollecting);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = true;
            hitCheck();
        }
        

        if (collision.gameObject.CompareTag("PowerUp") && isCollecting == true )
        {
            print("applied");

            collectedPowerUp = powerUpsSpawner.returnType();
            switch (collectedPowerUp )
            {
                case PowerUpType.Health:
                    if (enemy.playerHealth < 75)
                    {
                        isCollected = true;
                        if(enemy.playerHealth + 20 >= 75)
                        {
                            enemy.playerHealth += 20;
                        }
                        else
                        {
                            enemy.playerHealth = 75;
                        }
                        enemy.playerSlider.value = enemy.playerHealth;
                    }

                   
                    break;
                case PowerUpType.Damage:
                    StartCoroutine(DamagePowerUP());
                    isCollected = true;
                    break;
                case PowerUpType.Shield:
                    StartCoroutine(ShieldPowerUp());
                    isCollected = true;
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //print(collision.gameObject.tag);
        //print(isCollecting);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = true;
            hitCheck();
        }


        if (collision.gameObject.CompareTag("PowerUp") && isCollecting == true)
        {
            print("applied");
            collectedPowerUp = powerUpsSpawner.returnType();
            switch (collectedPowerUp)
            {
                case PowerUpType.Health:
                    if (enemy.playerHealth < 75)
                    {
                        if(enemy.playerHealth + 20 <= 75)
                        {
                            enemy.playerHealth += 20;
                        }
                        else
                        {
                            enemy.playerHealth = 75;
                        }
                        enemy.playerSlider.value = enemy.playerHealth;
                      
                    }

                    else if (enemy.playerHealth >= 75)
                    {
                        enemy.playerSlider.value = enemy.playerHealth;

                    }
                    
                  

                    break;
                case PowerUpType.Damage:
                    StartCoroutine(DamagePowerUP());
                    isCollected = true;
                    break;
                case PowerUpType.Shield:
                    StartCoroutine(ShieldPowerUp());
                    isCollected = true;
                    break;
            }
        }
    }

    private IEnumerator ShieldPowerUp()
    {
        shieldPowerActive = true;
      yield return new WaitForSeconds(20f);
        shieldPowerActive = false;
    }

    IEnumerator ResetP()
    {
        yield return new WaitForSeconds(0.5f);
        isCollecting = false;

    }
    

    private IEnumerator DamagePowerUP()
    {
        _health.damage *= 2;
        
        yield return new WaitForSeconds(10);
        _health.damage /= 2 ;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = false;
            hitCheck();
        }
    }

    public   Animator ReturnAnimator()
    {
        return animator;
    }
    public bool ReturnCollected()
    {
        return isCollected;
    }
    public IEnumerator Shoot(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            
            GameObject bul = Instantiate(bullet);
            if (Tutorial.tutFin != true)
            {
                if (tutEnemy.GetComponent<Enemy>().Distance() >= 0)
                {
                    if (Buttons.CharacterChossen == 1 || Buttons.CharacterChossen == 2)
                    {
                        bul.transform.position = (transform.position + new Vector3(2f, 2.5f, 0));
                    }
                    else if(Buttons.CharacterChossen == 4)
                    {
                        bul.transform.position = (transform.position + new Vector3(2f, 1.5f, 0));
                    }

                }
                else if (tutEnemy.GetComponent<Enemy>().Distance() <= 0)
                {
                    if (Buttons.CharacterChossen == 1 || Buttons.CharacterChossen == 2)
                    {
                        bul.transform.position = (transform.position + new Vector3(-2f, 2.5f, 0));
                        bul.transform.Rotate(0f, 180f, 0.0f, Space.World);
                    }
                    else if (Buttons.CharacterChossen == 4)
                    {
                        bul.transform.position = (transform.position + new Vector3(-2f, 1.5f, 0));
                        bul.transform.Rotate(0f, 180f, 0.0f, Space.World);

                    }
                }
              
            }
            else {
                if (enemy.Distance() >= 0)
                {
                    if (Buttons.CharacterChossen == 1 || Buttons.CharacterChossen == 2)
                    {
                        bul.transform.position = (transform.position + new Vector3(2f, 2.5f, 0));
                    }
                    else if (Buttons.CharacterChossen == 4)
                    {
                        bul.transform.position = (transform.position + new Vector3(2f, 1.5f, 0));
                    }

                }
                else if (enemy.Distance() <= -0)
                {
                    if (Buttons.CharacterChossen == 1 || Buttons.CharacterChossen == 2)
                    {
                        bul.transform.position = (transform.position + new Vector3(-2f, 2.5f, 0));
                        bul.transform.Rotate(0f, 180f, 0.0f, Space.World);

                    }
                    else if (Buttons.CharacterChossen == 4)
                    {
                        bul.transform.position = (transform.position + new Vector3(-2f, 1.5f, 0));
                        bul.transform.Rotate(0f, 180f, 0.0f, Space.World);

                    }
                }
           
            }

           

            yield return new WaitForSeconds(0.15f);
        }
    }

}
