using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;

    


    private LevelPick levelPick;
    private Rigidbody2D rb;

    private Animator animator;
    private int level;

    private GameObject brickManScript;
    


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

    
}
