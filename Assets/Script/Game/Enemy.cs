using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AnimatorOverrideController[] animatorOverrideControllers;

    [SerializeField] private GameObject player;


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

        if(level == 3 )
        {
            this.gameObject.transform.Rotate( 0.0f, 180f, 0.0f, Space.World);
        }

        animator.runtimeAnimatorController = animatorOverrideControllers[level - 1];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position + new Vector3(2, 0, 0), speed * Time.deltaTime);
    }
}
