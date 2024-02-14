using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;
    [SerializeField] private bool enemyActive;

    

    private LevelPick levelPick;
    private Rigidbody2D rb;

    private Animator animator;
    private int level;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        levelPick = new LevelPick();

        level = levelPick.Level();

        animator.runtimeAnimatorController = animatorOverrideControllers[level - 1];

        
    }


    void Update()
    {
        if (enemyActive)
        {
            if (level == 1)
            {
                this.gameObject.GetComponent<BrickManAI>().enabled = true;
            }
            else if (level == 2)
            {
                this.gameObject.GetComponent<EvilDarrenAI>().enabled = true;
            }
            else if (level == 3)
            {

            }
            else
            {

            }
        }
        else
        {
            this.gameObject.GetComponent<BrickManAI>().enabled = false;
            this.gameObject.GetComponent<EvilDarrenAI>().enabled = false;

        }
        

    }

    
}
