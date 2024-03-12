using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static bool tutFin;
    public   bool movement;
    public   bool basicAtk;
    public   bool specialAtk;
    public   bool block;
    public   bool powerUps;
    private  DialogueTrigger dialogueTrigger;
    TutTextManager textManager;
    Tutorial tut;
    internal bool refresh;

    private void Awake()
    {
        movement = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        textManager = FindAnyObjectByType<TutTextManager>();
        tut = textManager.GetComponent<Tutorial>();
        dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        dialogueTrigger.TriggerDialogue();
    }

    public void CheckIfTrue()
    {
        refresh = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
