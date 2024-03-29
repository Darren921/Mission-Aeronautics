using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    internal bool battleStart;
    internal bool battleComplete;

    private void Awake()
    {
        movement = true;
    }

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

    public void SkipTut()
    {
        tutFin = true;
        SceneManager.LoadScene("Levels");
        InputManager.DisableInGame();
    }
}
