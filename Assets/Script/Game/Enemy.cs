using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;

    private LevelPick levelPick;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
